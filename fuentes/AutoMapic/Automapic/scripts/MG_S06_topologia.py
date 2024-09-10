try:
    response = dict()
    response['status'] = 1
    response['message'] = 'success'
    
    # Importar librerias
    import arcpy
    import json
    import settings_aut as st
    import packages_aut as pkg
    import messages_aut as msg
    import os
    import automapic as aut
    import MG_S00_model as model

    arcpy.ClearWorkspaceCache_management()
    arcpy.ResetEnvironments()
    arcpy.env.overwriteOutput = True

    hojas = arcpy.GetParameterAsText(0)
    topologias = arcpy.GetParameterAsText(1)
    zona = arcpy.GetParameterAsText(2)
    # geodatabase = arcpy.GetParameterAsText(3)

    workspace = arcpy.env.scratchGDB

    def topology_overlaps(geologia):
        global workspace
        response = os.path.join(workspace, 'Overlaps')
        its = arcpy.Intersect_analysis(geologia, response)
        return response


    def topology_holes(geologia, hoja):
        global workspace
        geologia_diss = arcpy.Dissolve_management(geologia, 'in_memory\\geologia_diss')
        shape_diss = map(lambda i: i[0], arcpy.da.SearchCursor(geologia_diss, ["SHAPE@"]))[0]
        hojas_diss = arcpy.Dissolve_management(hoja, 'in_memory\\hojas_diss')
        shape_hoja = map(lambda i: i[0], arcpy.da.SearchCursor(hojas_diss, ["SHAPE@"]))[0]
        shape_hoja = shape_hoja.projectAs(shape_diss.spatialReference)
        symdif = shape_hoja.symmetricDifference(shape_diss)
        symdif_feature = arcpy.CopyFeatures_management(symdif, 'in_memory\\symdif')
        response = os.path.join(workspace, 'Holes')
        arcpy.MultipartToSinglepart_management(symdif_feature, response)
        return response


    # Se construye la consulta de hojas seleccionadas
    hojas = hojas.replace("'", '').replace(" ", '')
    query = "{} in ('{}')".format(st._CODHOJA_FIELD, "', '".join(hojas.split(',')))

    # Se construye la consulta de topologias seleccionadas
    topologias = topologias.replace("'", '').replace(" ", '')

    # Se obtiene el makefeaturelayer de las hojas seleccionadas
    # feature_path = st._ULITO_MG_PATH.format(zona, zona)
    feature_path = model.gpo_dgr_ulito(str(zona)).path
    feature_mfl = arcpy.MakeFeatureLayer_management(feature_path, 'ulitomfl', query)
    #feature_mfl = arcpy.Select_analysis(feature_path, 'in_memory\\ulitomfl', query)

    if not int(arcpy.GetCount_management(feature_mfl).getOutput(0)):
        raise RuntimeError(msg._ERROR_MG_NOT_DATA_ULITO)

    # Evaluando topologias
    df = pkg.get_topology_items(topologias, as_dataframe=True)
    container_response = list()
    for idx, row in df.iterrows():
        if row['id'] == 1:
            # Topologias overlaps
            result = topology_overlaps(feature_mfl)
            layer = os.path.join(st._LAYERS_DIR, 'GPO_MG_OVERLAPS.lyr')
        elif row['id'] == 2:
            # Topologia holes
            # cuadriculas = st._CUADRICULAS_MG_PATH
            hojas = arcpy.MakeFeatureLayer_management(st._CUADRICULAS_MG_PATH, 'hojas', query)
            #hojas = arcpy.Select_analysis(st._CUADRICULAS_MG_PATH, 'in_memory\\hojas', query)
            result = topology_holes(feature_mfl, hojas)
            layer = os.path.join(st._LAYERS_DIR, 'GPO_MG_HOLES.lyr')
        
        count = arcpy.GetCount_management(result).getOutput(0)
        text = "{}: se encontraron {} resultados".format(row['name'], count)
        container_response.append(text)
        name_feature = os.path.basename(result)
        aut.add_layer_with_new_datasource(layer, name_feature, workspace, 'FILEGDB_WORKSPACE', zoom=False)
        
    
    text_response = '\n'.join(container_response)
    response["response"] = text_response

except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    response = json.dumps(response, encoding='utf-8', ensure_ascii=False)
    arcpy.SetParameterAsText(3, response)
