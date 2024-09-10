import sys
import pythonaddins
reload(sys)
sys.setdefaultencoding("utf-8")
# Importar librerias
import arcpy
import json
import tempfile
import messages_aut as msg
import os
import settings_aut as st
import traceback
import uuid
#import PG_S01_mapa_peligros_geologicos as mpg
import arcobjects as arc

arcpy.env.overwriteOutput = True
scratchGDB = os.path.normpath(arcpy.env.scratchFolder)#.replace("\\","/")

response = dict()
response['status'] = 1
response['message'] = 'success'

def get_departament(layer_interes, layer_depa):
    """
    Obtiene el o los departamentos del mapa geologico <> 50000
    :param layer_interes: Capa de interes
    :param layer_depa: Capa de departamentos
    :return: Codigo del departamento
    """
    arcpy.SelectLayerByLocation_management(layer_depa, 'INTERSECT', layer_interes, '', 'NEW_SELECTION')
    if arcpy.GetCount_management(layer_depa).getOutput(0) == '1':
        cd_depa = arcpy.SearchCursor(layer_depa).next().getValue(st._CD_DEPA_FIELD)
        return [cd_depa]
        
    rows = [m[0] for m in  arcpy.da.SearchCursor(layer_depa, [st._CD_DEPA_FIELD])]
    if len(rows) == 0:
        return []
    return rows

def clip_area_interes(layer_interes, layer_interes2):
    """
    Recorta las capas de Geomorfologia con el area de interes del usuario y devuelve la lista
     de codigos CODI unicos
    """
    lyr_clip= arcpy.Clip_analysis(layer_interes, layer_interes2, os.path.join(scratchGDB, "Area_geomorfologica.shp"))
    #lista_codi = list(set([codi[0] for codi in arcpy.da.SearchCursor(lyr_clip,[field])]))
    return lyr_clip.getOutput(0)

def get_area_by_sheet(lyr_hojas):
    mxd_current = arcpy.mapping.MapDocument("current")
    df_current = arcpy.mapping.ListDataFrames(mxd_current)[0]
    lyr_hojas_current = arcpy.mapping.ListLayers(mxd_current,lyr_hojas, df_current)
    fc_dissolve = arcpy.Dissolve_management(lyr_hojas_current[0],os.path.join(scratchGDB, "gpo_area_interes.shp"), "", "", "MULTI_PART", "DISSOLVE_LINES")
    #layer= arcpy.mapping.Layer("in_memory/mapArea_geomorfologia")
    return fc_dissolve.getOutput(0)

if __name__=='__main__':
    try:
        # Insertar procesos 
        _ELM_MAP_TITLE = 'ELM_TITULO'
        _ELM_AUTHOR = 'ELM_AUTOR'
        _ELM_REVIEWER = 'ELM_REVISOR'
        _ELM_MAP_CODE = 'ELM_CODIGO'
        _ELM_SCALEBAR = 'ELM_BARRAESCALA'
        _ELM_PROJECTION = 'ELM_PROY'
        _NAME_LAYER_INTERES = 'gpo_area_interes'
        _NAME_LAYER_HOJAS = 'GPO_DG_HOJAS_50K'
        _NAME_LAYER_AREA_GEOM = 'Area_geomorfologica'
        # _NAME_LAYER_DEPARTAMENTO = 'GPO_DEP_DEPARTAMENTOS'
        # _NAME_LAYER_DEPARTAMENTO_AREA = 'GPO_DEP_DEPARTAMENTO'
       
        feature = arcpy.GetParameterAsText(0) #como opcional
        map_title = arcpy.GetParameterAsText(1)
        author = arcpy.GetParameterAsText(2)
        document = arcpy.GetParameterAsText(3)
        mode = arcpy.GetParameterAsText(4)
        source_geom= arcpy.GetParameterAsText(5)
        # scale = int(arcpy.GetParameterAsText(4))
        # orientation = arcpy.GetParameterAsText(5)
        # map_size = arcpy.GetParameterAsText(6)

        temp_folder = st._TEMP_FOLDER if st._TEMP_FOLDER else tempfile.gettempdir()
        if mode == "0":
            feature = get_area_by_sheet(_NAME_LAYER_HOJAS)

        desc = arcpy.Describe(feature)
        layer_extent = desc.extent
        epsg = desc.spatialReference.factoryCode
        dict_scr= {4326: 'GCS-WGS 84', 32717: 'UTM-WGS 84 ZONA 17', 32718: 'UTM-WGS 84 ZONA 18', 32719: 'UTM-WGS 84 ZONA 19'}

        mxd_name = 'T01_MGEOM_A3_H.mxd' #'T01_MGNO50K_{}_{}.mxd'.format(orientation, map_size)
        mxd_path = os.path.join(st._MXD_DIR, mxd_name.upper())
        
        mxd = arcpy.mapping.MapDocument(mxd_path)
        df_principal, df_ubicacion= arcpy.mapping.ListDataFrames(mxd)
        # lyrs_departamento = arcpy.mapping.ListLayers(mxd, _NAME_LAYER_DEPARTAMENTO, df_principal)
        lyrs_interes = arcpy.mapping.ListLayers(mxd, _NAME_LAYER_INTERES, df_principal)
        lyrs_interes2 = arcpy.mapping.ListLayers(mxd, _NAME_LAYER_HOJAS, df_principal)
        lyrs_interes3 = arcpy.mapping.ListLayers(mxd, _NAME_LAYER_AREA_GEOM, df_principal)
        # lyrs_interes4 = arcpy.mapping.ListLayers(mxd, _NAME_LAYER_POG, df_principal)
        df_principal.extent = layer_extent
        #df_principal.scale = df_principal.scale * 1.15
        # lyrs_departamento_area = arcpy.mapping.ListLayers(mxd, _NAME_LAYER_DEPARTAMENTO_AREA, df_ubicacion)

        shape_dir = os.path.dirname(feature)
        shape_name =  os.path.basename(feature)
        # arcpy.AddMessage(shape_dir+" - "+shape_name)
        #response['dir_name']=shape_dir+" - "+shape_name
        # if mode == "0":
        #     shape_name_without_ext = shape_name
            # pythonaddins.MessageBox(shape_name+ mode,'title')
        # else:
            # pythonaddins.MessageBox(shape_name+ mode,'title')
        shape_name_without_ext = shape_name.split('.')[0]
        arcpy.RefreshTOC()
        arcpy.RefreshActiveView()

        for i1 in lyrs_interes:
            # if mode== "0":
            #     dir_path= scratchGDB.split(".gdb")[0]
            #     i1.replaceDataSource(dir_path + ".gdb", "FILEGDB_WORKSPACE", shape_name_without_ext, False)
            # else:  
            i1.replaceDataSource(shape_dir, "SHAPEFILE_WORKSPACE", shape_name_without_ext, False)
            i1.visible = True

        for i in lyrs_interes2: # Hojas
            hojas = st._CUADRICULAS_MG_PATH
            dataset_hojas = hojas.split("\\")[-2]
            gdb_hojas= hojas.split(".gdb")[0]+".gdb"
            i.replaceDataSource(gdb_hojas, "FILEGDB_WORKSPACE", dataset_hojas + "//" + _NAME_LAYER_HOJAS, False)
            i.visible = True

        legend = arcpy.mapping.ListLayoutElements(mxd, "LEGEND_ELEMENT","Legend_geom")[0]
        legend.autoAdd = True
        for i3 in lyrs_interes3: #
            if source_geom == "0":
                clip_geom= clip_area_interes(st._GPO_GEOMORFOLOGIA, feature)
            else:
                #pythonaddins.MessageBox("update made",'title')
                clip_geom= clip_area_interes(source_geom, feature)
            #layer= arcpy.mapping.Layer(clip_geom)
            clip_dir = os.path.dirname(clip_geom)
            baseName= os.path.basename(clip_geom)
            #arcpy.mapping.AddLayer(df_principal,layer,"BOTTOM")
            #arcpy.AddMessage(clip_dir + "--" + baseName)
            #response['clip_bname'] = clip_dir + "--" + baseName
            i3.replaceDataSource(clip_dir, "SHAPEFILE_WORKSPACE", _NAME_LAYER_AREA_GEOM, False)
            i3.visible = True
            #legend.updateItem(i3, use_visible_extent=True)
            #pythonaddins.MessageBox("update made",'title')
            legend.autoAdd = False
            
        desc= arcpy.Describe(lyrs_interes3[0])
        src = desc.spatialReference
        factoryCode = src.factoryCode
        df_principal.spatialReference = src
        # clip_geom= clip_area_interes(st._GPO_GEOMORFOLOGIA, feature)#lyrs_interes[0])
        # layer= arcpy.mapping.Layer(clip_geom)
        # arcpy.mapping.AddLayer(df_principal,layer,"BOTTOM")
        # legend.updateItem(layer, use_visible_extent=True)
        # pythonaddins.MessageBox("update made",'title')
        # legend.autoAdd = False

        #Determinar departamento
        # cd_depas = get_departament(lyrs_interes[0], lyrs_departamento[0])
        # if cd_depas == []:
        #     raise RuntimeError(msg._MPM_ERROR_OUT_PERU)
        # arcpy.SelectLayerByAttribute_management(lyrs_departamento[0], "CLEAR_SELECTION")

        # lyrs_departamento_area[0].definitionQuery = "{} IN ({})".format(st._CD_DEPA_FIELD, ", ".join(["'{}'".format(cd_depa) for cd_depa in cd_depas]))

        # Modificacion de elementos texto de membrete
        text_elements = arcpy.mapping.ListLayoutElements(mxd, "TEXT_ELEMENT")

        for elm in text_elements:
            if elm.name == _ELM_MAP_TITLE:
                elm.text = map_title 
            elif elm.name == _ELM_AUTHOR:
                elm.text = author
            elif elm.name == _ELM_MAP_CODE:
                elm.text = document
            elif elm.name == _ELM_REVIEWER:
                elm.text = author
            elif elm.name== _ELM_PROJECTION:
                elm.text = dict_scr[factoryCode]

        # Habilitando la visualizacion de capas
        # for layer in arcpy.mapping.ListLayers(mxd, '*'):
        #     if layer.supports("VISIBLE"):
        #         if not layer.visible:
        #             layer.visible = True
        
        arcpy.RefreshTOC()
        arcpy.RefreshActiveView()
        output_dir_mxd = os.path.join(temp_folder, document)
        if os.path.exists(output_dir_mxd):
            import shutil
            shutil.rmtree(output_dir_mxd)
        os.mkdir(output_dir_mxd)
        name_out = r'{}_{}.mxd'.format(document, uuid.uuid4().hex)
        response['mxd'] = os.path.join(output_dir_mxd, name_out)
        mxd.saveACopy(response['mxd'])
        # Seleccionar la grilla del mapa principal
        arc.select_grid_by_name(response['mxd'], '{}'.format(factoryCode), exclude_grids=None)

        # response['scale_params'] = mpg.set_scale_bar(scale)
        # response['scale_params']['name_scale'] = _ELM_SCALEBAR

    except Exception as e:
        response['status'] = 0
        response['message'] = traceback.format_exc()
    finally:
        arcpy.AddMessage(response)
        response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
        arcpy.SetParameterAsText(6, response)
