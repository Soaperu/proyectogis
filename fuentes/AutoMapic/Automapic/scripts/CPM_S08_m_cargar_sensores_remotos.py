from CPM_S02_model import *
import arcpy
import packages_aut as pkg
import settings_aut as st
import messages_aut as msg
import os

arcpy.env.overwriteOutput = True

def check_geodatabase(geodatabase_path):
    desc = arcpy.Describe(geodatabase_path)
    if not desc.datatype.lower() == 'workspace':
        raise RuntimeError(msg._CPM_ERROR_GDB_TYPE)

def check_exist_feature(feature_path):
    if not arcpy.Exists(feature_path):
        name = os.path.basename(feature_path)
        raise RuntimeError(msg._CPM_FEATURE_NOT_EXIST.format(name))

def check_geometry_type(feature_path, geometry):
    desc = arcpy.Describe(feature_path)
    if desc.shapeType.lower() != geometry:
        raise RuntimeError(msg._CPM_ERROR_SHAPETYPE_FG)
        
    

if __name__ == '__main__':
    try:
        geodatabase = pkg.get_config_param_value(st._ENV_GDB_PATH_CPM, one=True)
        arcillas = arcpy.GetParameterAsText(0)
        oxidos = arcpy.GetParameterAsText(1)
        tipo = arcpy.GetParameterAsText(2)

        arcpy.AddMessage(msg._CPM_INIT_PROCESS)
        arcpy.AddMessage(msg._CPM_CHECK_GEODATABASE)
        check_geodatabase(geodatabase)

        fc_remsem = pmm_gpo_sensores(geodatabase) if tipo == 'metalico' else rmi_gpo_sensores(geodatabase)

        check_exist_feature(fc_remsem.path)
        check_geometry_type(arcillas, fc_remsem.shapeType)
        check_geometry_type(oxidos, fc_remsem.shapeType)
        
        # Dissolve
        # Dissolve arcillas
        fc_arcillas_diss_out = 'in_memory\\arcillas'
        fc_arcillas = arcpy.Dissolve_management(arcillas, fc_arcillas_diss_out, "#", '#', 'MULTI_PART', 'DISSOLVE_LINES')

        # Dissolve oxidos
        fc_oxidos_diss_out = 'in_memory\\oxidos'
        fc_oxidos = arcpy.Dissolve_management(oxidos, fc_oxidos_diss_out, "#", '#', 'MULTI_PART', 'DISSOLVE_LINES')

        # Modificar campos
        # Modificar campos del featurelayer arcillas
        fields_arcillas = arcpy.ListFields(fc_arcillas)
        for field in fields_arcillas:
            try:
                arcpy.DeleteField_management(fc_arcillas, field.name)
            except:
                pass

        arcpy.AddField_management(fc_arcillas, fc_remsem.tipo_arc, "TEXT")
        
        # Modificar campos del featurelayer oxidos
        fields_oxidos = arcpy.ListFields(fc_oxidos)
        for field in fields_oxidos:
            try:
                arcpy.DeleteField_management(fc_oxidos, field.name)
            except:
                pass  
        
        arcpy.AddField_management(fc_oxidos, fc_remsem.tipo_oxi, "TEXT")
        
        # Actualizando atributos
        arcpy.CalculateField_management(fc_arcillas, fc_remsem.tipo_arc, '"arcillas"')
        arcpy.CalculateField_management(fc_oxidos, fc_remsem.tipo_oxi, '"oxidos"')

        # Union de featurelayer de arcillas y oxidos
        feature_layer_union = [fc_arcillas, fc_oxidos]
        feature_layer_union_path = 'in_memory\\union'
        union = arcpy.Union_analysis(feature_layer_union, feature_layer_union_path, 'ALL', '#', 'GAPS')

        arcpy.AddField_management(union, fc_remsem.tipo, "TEXT")

        fields_sr = [fc_remsem.tipo, fc_remsem.tipo_arc, fc_remsem.tipo_oxi]
        with arcpy.da.UpdateCursor(union, fields_sr) as cursor_uc:
            for x in cursor_uc:
                if x[1] != "" and x[2] != "":
                    x[0] = "{} - {}".format(x[1], x[2])
                elif x[1] != "" and x[2] == "":
                    x[0] = x[1]
                    x[2] = '-'
                elif x[1] == "" and x[2] != "":
                    x[0] = x[2]
                    x[1] = '-'
                cursor_uc.updateRow(x)
        del cursor_uc

        all_fields_sr = arcpy.ListFields(fc_remsem.path)
        for i in all_fields_sr:
            try:
                if i.name not in fields_sr:
                    arcpy.DeleteField_management(union, i.name)
            except:
                pass
        
        arcpy.AddMessage(msg._CPM_SEND_DATA_TO_GEODATABASE)
        arcpy.DeleteRows_management(fc_remsem.path)
        arcpy.Append_management(union, fc_remsem.path, "NO_TEST")

        arcpy.AddMessage(msg._CPM_END_PROCESS)
        arcpy.SetParameterAsText(3, fc_remsem.path)

    except Exception as e:
        arcpy.AddError('\n\t%s\n' % e.message)