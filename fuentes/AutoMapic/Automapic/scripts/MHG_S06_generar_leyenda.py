import arcpy
import json
import settings_aut as st
import messages_aut as msg
import pandas as pd
import automapic_template_json as auttmp
import automapic as aut
import os
import PG_S01_mapa_peligros_geologicos as pgeo
import numpy as np
import uuid

arcpy.env.overwriteOutput = True

response = dict()
response['status'] = 1
response['message'] = 'success'

data = arcpy.GetParameterAsText(0)
clasificacion = arcpy.GetParameterAsText(1)
id_mapa = arcpy.GetParameterAsText(2)
df_nombre = arcpy.GetParameterAsText(3)

guid = uuid.uuid4().hex


def get_df_legend_aux():
    if not arcpy.Exists(st._TB_01_LEYENDA_AUX_PATH):
        raise RuntimeError()
    n_arr = arcpy.da.TableToNumPyArray(st._TB_01_LEYENDA_AUX_PATH, ["*"], null_value=None)
    df = pd.DataFrame(n_arr)
    return df

def get_coords_container(*args):
    extent = arcpy.Extent(*args)
    extent_pol_json = json.loads(extent.polygon.JSON)
    return extent_pol_json["rings"]

def generate_row(objectid, descrip, id_mapa, coords):
    row = {"attributes": {"OBJECTID": objectid, "tipo": descrip, "id_mapa": id_mapa}, "geometry": {"rings": coords}}
    return row

def get_coords_point(container, xmin, ymin, xmax, ymax):
    if container["haligntext"].item() == "left":
        x = xmin + container["spaceh"].item()
    elif container["haligntext"].item() == "right":
        x = xmax - container["spaceh"].item()
    elif container["haligntext"].item() == "middle":
        # Corregido
        x = xmin + (0.5 * abs(abs(xmax) - abs(xmin)))
    if container["valigntext"].item() == "top":
        y = ymax - container["spacev"].item()
    elif container["valigntext"].item() == "middle":
        # corregido
        y = ymin + (0.5 * abs(abs(ymax) - abs(ymin)))
    elif container["valigntext"].item() == "bottom":
        y = ymin + container["spacev"].item()
    return {"x": x, "y": y}

def case_text_decorator(func):
    def decorator(*args, **kwargs):
        text = args[0]
        if kwargs['case']:
            text = func(*args, **kwargs)
            try:
                text = getattr(text, kwargs['case'])()
            except:
                pass
        if kwargs['strip']:
            text = text.strip()
        return text
    return decorator

@case_text_decorator
def get_name_component(code, case=None, strip=True):
    query = "{} = '{}'".format(st._ID, code)
    cursor = arcpy.da.SearchCursor(st._TB_01_UNIDAD_HIDROGEOLOGICA_PATH, [st._NOMBRE_FIELD], query)
    data = map(lambda i: i[0], cursor)
    response = data[0] if len(data) else code
    return response

@case_text_decorator
def set_text(text, case=None, strip=True):
    return text
    
try:

    query = "{} = '{}'".format(st._ID_MAPA, id_mapa)
    y_controller = 0

    data = json.loads(data)
    df = pd.DataFrame(data["Table1"])

    df[st._ID_MAPA] = id_mapa

    clasificacion = json.loads(clasificacion)

    # Obteniendo la tabla de datos auxiliares 
    df_aux = get_df_legend_aux()

    uhidrog_p = df_aux.loc[df_aux["nombre"] == "bx_uhidrog"]
    chidrog_p = df_aux.loc[df_aux["nombre"] == "bx_chidrog"]
    litologia_p = df_aux.loc[df_aux["nombre"] == "bx_litologia"]
    descrip_p = df_aux.loc[df_aux["nombre"] == "bx_descrip"]
    chidrog_name_p = df_aux.loc[df_aux["nombre"] == "bx_chidrog_name"]
    fhidrog_p = df_aux.loc[df_aux["nombre"] == "bx_fhidrog"]
    fhidrog_symb_p = df_aux.loc[df_aux["nombre"] == "bx_fhidrog_symb"]
    uhidrog_name_p = df_aux.loc[df_aux["nombre"] == "bx_uhidrog_name"]
    litologia_name_p = df_aux.loc[df_aux["nombre"] == "bx_litologia_name"]
    descrip_name_p = df_aux.loc[df_aux["nombre"] == "bx_descrip_name"]
    fhidrog_name_p = df_aux.loc[df_aux["nombre"] == "bx_fhidrog_name"]
    fhidrog_desc_p = df_aux.loc[df_aux["nombre"] == "bx_fhidrog_desc"]


    # Autocompletando columnas de unidad hidrogeologica, clasificacion y subclasificacion
    df[st._UHIDROG] = df["ID"].str[0]
    df[st._CL_HIDROG] = df["ID"].str[:2]
    df[st._SCL_HIDROG] = df["ID"].str[2]
    df["CLASE"] = df.apply(lambda row: row[st._CL_HIDROG] if row[st._SCL_HIDROG] == "0" else row["ID"][:3], axis=1)


    # Obteniendo el numero de formaciones hidrogeologicas
    n_form = len(df)

    # Obteniendo las unidades hidrogeologicas unicas
    uhidrog = list(df[st._UHIDROG].unique())
    uhidrog.sort()

    # Obteniendo el numero de clasificaciones
    n_class = len(df[st._CL_HIDROG].unique())

    # Obteniendo el ancho y alto de la leyenda
    w_uhidrog = uhidrog_p["width"].item()
    w_cl_hidrog = chidrog_p["width"].item()
    w_litologia = litologia_p["width"].item()
    w_descrip = descrip_p["width"].item()
    width_tot = w_uhidrog + w_cl_hidrog + w_litologia + w_descrip


    # Altura total de la leyenda
    height_head = uhidrog_p["height"].item()
    height_fhidrog = fhidrog_symb_p["height"].item()
    space_v = fhidrog_symb_p["spacev"].item()
    height_tot = (n_form * space_v) + (n_form * height_fhidrog) + (n_class * space_v) +  height_head


    # Obteniendo las coordenadas que definen el extent de la leyenda
    mxd = arcpy.mapping.MapDocument("CURRENT")
    dframes = arcpy.mapping.ListDataFrames(mxd, "*{}*".format(df_nombre))
    if not len(dframes):
        raise RuntimeError(msg._ERROR_NO_SUCH_DATAFRAME)
    dframe = dframes[0]
    dframe.spatialReference = arcpy.SpatialReference(4326)
    extent_proj = dframe.extent.polygon.projectAs(arcpy.SpatialReference(32718))
    x_ini, y_ini = extent_proj.extent.XMin, extent_proj.extent.YMin
    x_fin, y_fin = x_ini + width_tot, y_ini + height_tot

    dframe.spatialReference = arcpy.SpatialReference(32718)
    arcpy.RefreshActiveView()
    arcpy.RefreshTOC()

    arcpy.AddMessage("{} {} {} {}".format(x_ini, y_ini, x_fin, y_fin))


    pt_data = list()
    po_data = list()

    h_fhidrog = fhidrog_symb_p["height"].item()
    space = fhidrog_symb_p["spacev"].item()

    # Coordenada X maxima de unidad hidrogeologica
    x_fin_uh = x_ini + uhidrog_p["width"].item()

    # 
    x_ini_chidrog = x_fin_uh
    x_fin_chidrog = x_ini_chidrog + chidrog_p["width"].item()

    # Coordenadas X limite de los contenedores de clasificacion hidrogeologica
    x_ini_box_cl_name = x_fin_uh
    x_fin_cl_name = x_ini_box_cl_name + chidrog_name_p["width"].item()

    # Coordenadas X limite de los contenedores de formaciones hidrogeologicas
    x_ini_box_fhidrog = x_fin_cl_name
    x_fin_box_fhidrog = x_ini_box_fhidrog + fhidrog_p["width"].item()

    # Coordenadas X limite de los contenedores de litologia
    x_ini_box_litologia = x_fin_box_fhidrog
    x_fin_box_litologia = x_ini_box_litologia + litologia_p["width"].item()

    # Coordenadas X limite de los contenedores de descripcion de las clasificacione shidrogeologicas
    x_ini_box_descrip = x_fin_box_litologia
    x_fin_box_descrip = x_ini_box_descrip + descrip_p["width"].item()

    # Coordenadas X limite de las cuadriculas de formaciones hidrogeologicas
    x_ini_box_hfidrog_s = x_fin_cl_name + space
    x_fin_box_hfidrog_s =  x_ini_box_hfidrog_s + fhidrog_symb_p["width"].item()


    contador = 0

    # :HEADER
    # Unidad Hidrogeologica
    # Container
    contador += 1
    coords = get_coords_container(x_ini, y_fin - uhidrog_p["height"].item(), x_fin_uh, y_fin)
    row = generate_row(contador, "border", id_mapa, coords)
    po_data.append(row)
    # Etiqueta
    contador += 1
    coord = get_coords_point(uhidrog_p, x_ini, y_fin - uhidrog_p["height"].item(), x_fin_uh, y_fin)
    etiqueta = set_text(uhidrog_p["text"].item(), case=uhidrog_p["casetext"].item(), strip=True)
    row = {"attributes": {"OBJECTID": contador, "etiqueta": etiqueta, "tipo": "head", "id_mapa": id_mapa}, "geometry": coord}
    pt_data.append(row)


    # Clasificacion hidrogeologica
    # Container
    contador += 1
    coords = get_coords_container(x_ini_chidrog, y_fin - chidrog_p["height"].item(), x_fin_chidrog, y_fin)
    row = generate_row(contador, "border", id_mapa, coords)
    po_data.append(row)
    # Etiqueta
    contador += 1
    coord = get_coords_point(chidrog_p, x_ini_chidrog, y_fin - chidrog_p["height"].item(), x_fin_chidrog, y_fin)
    etiqueta = set_text(chidrog_p["text"].item(), case=chidrog_p["casetext"].item(), strip=True)
    row = {"attributes": {"OBJECTID": contador, "etiqueta": etiqueta, "tipo": "head", "id_mapa": id_mapa}, "geometry": coord}
    pt_data.append(row)


    # Unidad litologia
    # Container
    contador += 1
    coords = get_coords_container(x_ini_box_litologia, y_fin - litologia_p["height"].item(), x_fin_box_litologia, y_fin)
    row = generate_row(contador, "border", id_mapa, coords)
    po_data.append(row)
    # Etiqueta
    contador += 1
    coord = get_coords_point(litologia_p, x_ini_box_litologia, y_fin - litologia_p["height"].item(), x_fin_box_litologia, y_fin)
    etiqueta = set_text(litologia_p["text"].item(), case=litologia_p["casetext"].item(), strip=True)
    row = {"attributes": {"OBJECTID": contador, "etiqueta": etiqueta, "tipo": "head", "id_mapa": id_mapa}, "geometry": coord}
    pt_data.append(row)


    # Descripcion
    # Container
    contador += 1
    coords = get_coords_container(x_ini_box_descrip, y_fin - descrip_p["height"].item(), x_fin_box_descrip, y_fin)
    row = generate_row(contador, "border", id_mapa, coords)
    po_data.append(row)
    # Etiqueta
    contador += 1
    coord = get_coords_point(descrip_p, x_ini_box_descrip, y_fin - descrip_p["height"].item(), x_fin_box_descrip, y_fin)
    etiqueta = set_text(descrip_p["text"].item(), case=descrip_p["casetext"].item(), strip=True)
    row = {"attributes": {"OBJECTID": contador, "etiqueta": etiqueta, "tipo": "head", "id_mapa": id_mapa}, "geometry": coord}
    pt_data.append(row)


    y_controller = y_fin - uhidrog_p["height"].item()


    for uh in uhidrog:
        chidrog = df.loc[df[st._UHIDROG] == uh]
        chidrog = chidrog[st._CL_HIDROG].unique()
        chidrog.sort()
        y_top_uh = y_controller
        for ch in chidrog:
            fhidrog = df.loc[df[st._CL_HIDROG] == ch]
            fhidrog = fhidrog.sort(["ID"])
            y_top_cl = y_controller
            for idx, val in fhidrog.iterrows():
                # Construccion de las cuadriculas que representan las formaciones hidrogeologicas
                contador += 1
                y_abs = y_controller - space
                coords = get_coords_container(x_ini_box_hfidrog_s, y_abs - h_fhidrog, x_fin_box_hfidrog_s, y_abs)
                row = generate_row(contador, val["CLASE"], id_mapa, coords)
                po_data.append(row)
                y_controller = y_abs - h_fhidrog

                # Construccion de etiquetas de formaciones hidrogeologicas
                contador += 1
                coord = {'x': x_fin_box_hfidrog_s + fhidrog_name_p["spaceh"].item(), 'y': y_abs - (h_fhidrog*0.5)}
                row = {"attributes": {"OBJECTID": contador, "etiqueta": val["Nombre"], "tipo": "nombre", "id_mapa": id_mapa}, "geometry": coord}
                pt_data.append(row)

                # Construccion de etiquetas de descripcion de formaciones hidrogeologicas
                contador += 1
                coord = {'x': x_fin_cl_name + fhidrog_desc_p["width"].item(), 'y': y_abs - (h_fhidrog*0.5)}
                etiqueta = set_text(val["Descripcion"], case=fhidrog_desc_p["casetext"].item(), strip=True)
                row = {"attributes": {"OBJECTID": contador, "etiqueta": etiqueta, "tipo": "descripcion", "id_mapa": id_mapa}, "geometry": coord}
                pt_data.append(row)

                # Construccion de etiquetas de litologia
                contador += 1
                coord = {'x': x_ini_box_litologia + litologia_name_p["spaceh"].item(), 'y': y_abs - (h_fhidrog*0.5)}
                etiqueta = set_text(val["Litologia"], case=None, strip=True)
                etiqueta = pgeo.set_detalle(etiqueta, litologia_name_p["ncharts"].item(), sep='|')
                row = {"attributes": {"OBJECTID": contador, "etiqueta": etiqueta, "tipo": "litologia", "id_mapa": id_mapa}, "geometry": coord}
                pt_data.append(row)



            
            y_controller = y_controller - space
            
            # Construccion del contener de clasificacion hidrogeologica
            contador += 1
            clBoxName_coords = get_coords_container(x_ini_box_cl_name, y_controller, x_fin_cl_name, y_top_cl)
            row = generate_row(contador, "border", id_mapa, clBoxName_coords)
            po_data.append(row)

            # Construccion de etiquetas de clasificacion hidrogeologica
            contador += 1
            coord = get_coords_point(chidrog_name_p, x_ini_box_cl_name, y_controller, x_fin_cl_name, y_top_cl)
            etiqueta = get_name_component(ch, case=chidrog_name_p["casetext"].item(), strip=True)
            row = {"attributes": {"OBJECTID": contador, "etiqueta": etiqueta, "tipo": "clasificacion", "id_mapa": id_mapa}, "geometry": coord}
            pt_data.append(row)

            # Construccion del contenedor de formaciones hidrogeologicas de una clase
            contador += 1
            clBoxSymb_coords = get_coords_container(x_ini_box_fhidrog, y_controller, x_fin_box_fhidrog, y_top_cl)
            row = generate_row(contador, "border", id_mapa, clBoxSymb_coords)
            po_data.append(row)

            # Construccion del contenedor de litologia
            contador += 1
            litBox_coords = get_coords_container(x_ini_box_litologia, y_controller, x_fin_box_litologia, y_top_cl)
            row = generate_row(contador, "border", id_mapa, litBox_coords)
            po_data.append(row)

            # Construccion del contenedor de descripcion de las clasificaciones hidrogeologicas
            contador += 1
            desBox_coords = get_coords_container(x_ini_box_descrip, y_controller, x_fin_box_descrip, y_top_cl)
            row = generate_row(contador, "border", id_mapa, desBox_coords)
            po_data.append(row)

            # Construccion de etiquetas de descripcion de las clasificaciones hidrogeologicas
            contador += 1
            coord = get_coords_point(descrip_name_p, x_ini_box_descrip, y_controller, x_fin_box_descrip, y_top_cl)
            # arcpy.AddMessage(clasificacion[ch])
            etiqueta = pgeo.set_detalle(clasificacion[ch], descrip_name_p["ncharts"].item(), sep='|')
            row = {"attributes": {"OBJECTID": contador, "etiqueta": etiqueta, "tipo": "descripcion hidrogeologica", "id_mapa": id_mapa}, "geometry": coord}
            pt_data.append(row)


        
        # Construccion del contenedor de unidad hidrogeologica
        contador += 1
        uhBox_coords = get_coords_container(x_ini, y_controller, x_fin_uh, y_top_uh)
        row = generate_row(contador, "border", id_mapa, uhBox_coords)
        po_data.append(row)

        # Contruccion de etiquetas del contenedor de unidad hidrogeologica 
        coord = get_coords_point(uhidrog_name_p, x_ini, y_controller, x_fin_uh, y_top_uh)
        etiqueta = get_name_component(uh, case=uhidrog_name_p["casetext"].item(), strip=True)
        row = {"attributes": {"OBJECTID": contador, "etiqueta": etiqueta, "tipo": "unidad hidrogeolica", "id_mapa": id_mapa}, "geometry": coord}
        pt_data.append(row)


    auttmp._PO_LEYENDA_TEMPLATE_MHG["features"] = po_data
    po_feature = arcpy.AsShape(auttmp._PO_LEYENDA_TEMPLATE_MHG, True)
    po_leyenda_mfl = arcpy.MakeFeatureLayer_management(st._PO_01_LEYENDA_DIVISIONES_PATH, 'po_leyenda_path_{}'.format(id_mapa), query)
    arcpy.DeleteRows_management(po_leyenda_mfl)
    arcpy.Append_management(po_feature, st._PO_01_LEYENDA_DIVISIONES_PATH, "NO_TEST")

    auttmp._PT_LEYENDA_TEMPLATE_MHG["features"] = pt_data
    pt_feature = arcpy.AsShape(auttmp._PT_LEYENDA_TEMPLATE_MHG, True)
    pt_leyenda_mfl = arcpy.MakeFeatureLayer_management(st._PT_01_LEYENDA_ETIQUETAS_PATH, 'pt_leyenda_path_{}'.format(id_mapa), query)
    arcpy.DeleteRows_management(pt_leyenda_mfl)
    arcpy.Append_management(pt_feature, st._PT_01_LEYENDA_ETIQUETAS_PATH, "NO_TEST")

    name_pt_leyenda = os.path.basename(st._PT_01_LEYENDA_ETIQUETAS_PATH)
    lyer_pt = os.path.join(st._BASE_DIR, 'layers/{}.lyr'.format(name_pt_leyenda))

    name_po_leyenda = os.path.basename(st._PO_01_LEYENDA_DIVISIONES_PATH)
    lyer_po = os.path.join(st._BASE_DIR, 'layers/{}.lyr'.format(name_po_leyenda))

    aut.add_layer_with_new_datasource(lyer_pt, name_pt_leyenda, st._GDB_PATH_HG, "FILEGDB_WORKSPACE", df_name=df_nombre, query=query)
    aut.add_layer_with_new_datasource(lyer_po, name_po_leyenda, st._GDB_PATH_HG, "FILEGDB_WORKSPACE", df_name=df_nombre, query=query, zoom=True, scale=2500)

    # configurando dataframe pandas para almacenar datos
    df[st._CODI_G] = df["ID"].str[-4:]
    df[st._ESTADO] = 1

    rename_columns = dict()
    rename_columns["ID"] = st._ID_FHIDROG
    rename_columns["Descripcion"] = st._D_FHIDROG
    rename_columns["Litologia"] = st._LITOLOGIA_G
    rename_columns["Nombre"] = st._N_FHIDROG
    df.rename(columns=rename_columns, inplace=True)

    df[st._CODI_G] = df[st._CODI_G].astype(int)

    arr = df.to_records(index=False)
    dt = arr.dtype
    descr = dt.descr
    # Es necesario reemplazar las columnas de tipo Object a unicode para ser legibles por numpy
    for i in range(len(descr)):
        if descr[i][1] == '|O':
            descr[i] = (descr[i][0], '<U300')

    dt = np.dtype(descr)
    arr = arr.astype(dt)
    output_table = os.path.join(arcpy.env.scratchGDB, "tb_leyenda_{}".format(guid))
    arcpy.da.NumPyArrayToTable(arr, output_table)

    # arcpy.env.overwriteOutput = True
    arcpy.AddMessage(st._TB_01_LEYENDA_PATH)
    tb_leyenda_tv = arcpy.MakeTableView_management(st._TB_01_LEYENDA_PATH, 'tb_leyenda_{}'.format(guid), query)
    arcpy.DeleteRows_management(tb_leyenda_tv)
    arcpy.Append_management(output_table, st._TB_01_LEYENDA_PATH, "NO_TEST")


    # Cargando datos de clasificacion
    tb_clasificacion = arcpy.MakeTableView_management(st._TB_01_CLASIFICACION_DESCRIPCION, 'tb_clasificacion_{}'.format(guid), query)
    arcpy.DeleteRows_management(tb_clasificacion)

    fields = [st._CL_HIDROG, st._DESCRIP, st._ID_MAPA]
    cursor = arcpy.da.InsertCursor(st._TB_01_CLASIFICACION_DESCRIPCION, fields)

    for k, v in clasificacion.items():
        cursor.insertRow((k, v, id_mapa))
    del cursor
except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
    arcpy.SetParameterAsText(4, response)
