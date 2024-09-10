import arcpy
import json
import settings_aut as st
import messages_aut as msg

response = dict()
response['status'] = 1
response['message'] = 'success'

valores_texto = arcpy.GetParameterAsText(0)
lista_valores = valores_texto.split(";")

_ELM_TITLE = "ELM_TITLE"
_ELM_PROYECTO = "ELM_PROYECTO"
_ELM_SUBTITLE = "ELM_SUBTITLE"
_ELM_AUTOR = "ELM_AUTOR"

try:
    # Insertar procesos
    mxd = arcpy.mapping.MapDocument("current")
    elm_proyecto = arcpy.mapping.ListLayoutElements(mxd, "TEXT_ELEMENT",_ELM_PROYECTO)[0]
    elm_title = arcpy.mapping.ListLayoutElements(mxd, "TEXT_ELEMENT",_ELM_TITLE)[0]
    elm_subtitle = arcpy.mapping.ListLayoutElements(mxd, "TEXT_ELEMENT",_ELM_SUBTITLE)[0]
    elm_autor = arcpy.mapping.ListLayoutElements(mxd, "TEXT_ELEMENT",_ELM_AUTOR)[0]

    elm_proyecto.text = lista_valores[0]
    elm_title.text = lista_valores[1]
    elm_subtitle.text = lista_valores[2]
    elm_autor.text = lista_valores[3]

    arcpy.RefreshActiveView()
    mxd.save()

except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
    arcpy.SetParameterAsText(1, response)
