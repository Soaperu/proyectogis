# -*-coding: utf-8-*-

_PL_PERFIL_TEMPLATE = {
  "displayFieldName": "",
  "fieldAliases": {
    "CODI": "CODI",
    "HOJA": "HOJA",
    "DESCRIP": "DESCRIP",
    "CUADRANTE": "CUADRANTE",
    "CODHOJA": "CODHOJA",
    "ETIQUETA": "ETIQUETA"
  },
  "geometryType": "esriGeometryPolyline",
  "fields": [
    {
      "name": "CODI",
      "type": "esriFieldTypeSmallInteger",
      "alias": "CODI",
      "length": 5
    },
    {
      "name": "HOJA",
      "type": "esriFieldTypeString",
      "alias": "HOJA",
      "length": 4
    },
    {
      "name": "CUADRANTE",
      "type": "esriFieldTypeString",
      "alias": "CUADRANTE",
      "length": 1
    },
    {
      "name": "CODHOJA",
      "type": "esriFieldTypeString",
      "alias": "CODHOJA",
      "length": 4
    },
    {
      "name": "ETIQUETA",
      "type": "esriFieldTypeString",
      "alias": "ETIQUETA",
      "length": 100
    },
    {
      "name": "DESCRIP",
      "type": "esriFieldTypeString",
      "alias": "DESCRIP",
      "length": 60
    }
  ]
}


_PT_LEYENDA_TEMPLATE_MHG = {
  "displayFieldName": "",
  "fieldAliases": {
    "OBJECTID": "OBJECTID",
    "etiqueta": "etiqueta",
    "tipo": "tipo",
    "id_mapa": "id_mapa"
  },
  "geometryType": "esriGeometryPoint",
  "fields": [
    {
      "name": "OBJECTID",
      "type": "esriFieldTypeOID",
      "alias": "OBJECTID"
    },
    {
      "name": "etiqueta",
      "type": "esriFieldTypeString",
      "alias": "etiqueta",
      "length": 500
    },
    {
      "name": "tipo",
      "type": "esriFieldTypeString",
      "alias": "tipo",
      "length": 50
    },
    {
      "name": "id_mapa",
      "type": "esriFieldTypeString",
      "alias": "id_mapa",
      "length": 50
    }
  ]
}

_PO_LEYENDA_TEMPLATE_MHG = {
  "displayFieldName": "",
  "fieldAliases": {
    "id_mapa": "id_mapa",
    "SHAPE_Length": "SHAPE_Length",
    "SHAPE_Area": "SHAPE_Area"
  },
  "geometryType": "esriGeometryPolygon",
  "fields": [
    {
      "name": "tipo",
      "type": "esriFieldTypeString",
      "alias": "descrip",
      "length": 50
    },
    {
      "name": "id_mapa",
      "type": "esriFieldTypeString",
      "alias": "id_mapa",
      "length": 50
    },
    {
      "name": "SHAPE_Length",
      "type": "esriFieldTypeDouble",
      "alias": "SHAPE_Length"
    },
    {
      "name": "SHAPE_Area",
      "type": "esriFieldTypeDouble",
      "alias": "SHAPE_Area"
    }
  ]
}

_PO_LEYENDA_TEMPLATE_MG = {
    "displayFieldName": "",
    "fieldAliases": {
        "CODI": "CODI",
        "ETIQUETA": "ETIQUETA",
        "UNIDAD": "UNIDAD",
        "DESCRIP": "Descripción",
        # "SERIE": "SERIE",
        "TIPO": "Tipo",
        "DOMINIO": "DOMINIO",
        # "ORDEN": "ORDEN",
        # "GROSOR_M": "Grosor medido",
        # "GROSOR_I": "Grosor indirecto",
        # "GROSOR_U": "Grosor usuario",
        "CODHOJA": "CODHOJA",
        # "SEP": "Separador",
        "ESTADO": "ESTADO",
        # "SHAPE_Length": "SHAPE_Length",
        # "SHAPE_Area": "SHAPE_Area"
    },
    "geometryType": "esriGeometryPolygon",
    "spatialReference": {
        "wkid": 32718,
        "latestWkid": 32718
    },
    "fields": [{
        "name": "CODI",
        "type": "esriFieldTypeInteger",
        "alias": "CODI"
    }, {
        "name": "ETIQUETA",
        "type": "esriFieldTypeString",
        "alias": "ETIQUETA",
        "length": 20
    }, {
        "name": "UNIDAD",
        "type": "esriFieldTypeString",
        "alias": "UNIDAD",
        "length": 50
    }, {
        "name": "DESCRIP",
        "type": "esriFieldTypeString",
        "alias": "Descripción",
        "length": 500
    }, {
        "name": "TIPO",
        "type": "esriFieldTypeString",
        "alias": "Tipo",
        "length": 50
    }, {
        "name": "DOMINIO",
        "type": "esriFieldTypeSmallInteger",
        "alias": "DOMINIO"
    }, {
        "name": "CODHOJA",
        "type": "esriFieldTypeString",
        "alias": "CODHOJA",
        "length": 5
    }, {
        "name": "ESTADO",
        "type": "esriFieldTypeSmallInteger",
        "alias": "ESTADO"
    }]
}


_PL_LEYENDA_TEMPLATE_MG = {
    "displayFieldName": "",
    "fieldAliases": {
        # "OBJECTID": "OBJECTID",
        "CODI": "CODI",
        "TIPO": "Tipo",
        "DOMINIO": "DOMINIO",
        "HOJA": "HOJA",
        "CUADRANTE": "CUADRANTE",
        "CODHOJA": "CODHOJA",
        "ESTADO": "ESTADO",
        # "SHAPE_Length": "SHAPE_Length"
    },
    "geometryType": "esriGeometryPolyline",
    "spatialReference": {
        "wkid": 32718,
        "latestWkid": 32718
    },
    "fields": [
      # {
      #   "name": "OBJECTID",
      #   "type": "esriFieldTypeOID",
      #   "alias": "OBJECTID"
      # }, 
    {
        "name": "CODI",
        "type": "esriFieldTypeInteger",
        "alias": "CODI"
    }, {
        "name": "TIPO",
        "type": "esriFieldTypeString",
        "alias": "Tipo",
        "length": 50
    }, {
        "name": "DOMINIO",
        "type": "esriFieldTypeSmallInteger",
        "alias": "DOMINIO"
    }, {
        "name": "HOJA",
        "type": "esriFieldTypeString",
        "alias": "HOJA",
        "length": 4
    }, {
        "name": "CUADRANTE",
        "type": "esriFieldTypeString",
        "alias": "CUADRANTE",
        "length": 1
    }, {
        "name": "CODHOJA",
        "type": "esriFieldTypeString",
        "alias": "CODHOJA",
        "length": 5
    }, {
        "name": "ESTADO",
        "type": "esriFieldTypeSmallInteger",
        "alias": "ESTADO"
    }
    ]
}

_PT_LEYENDA_TEMPLATE_MG = {
    "displayFieldName": "",
    "fieldAliases": {
        "ETIQUETA": "ETIQUETA",
        "ESTILO": "ESTILO",
        "ANGULO": "ANGULO",
        "ALINEACION": "ALINEACION",
        "TIPO": "Tipo",
        "DOMINIO": "DOMINIO",
        "ESTADO": "ESTADO",
        "HOJA": "HOJA",
        "CUADRANTE": "CUADRANTE",
        "CODHOJA": "CODHOJA"
    },
    "geometryType": "esriGeometryPoint",
    "spatialReference": {
        "wkid": 32718,
        "latestWkid": 32718
    },
    "fields": [
      {
          "name": "ETIQUETA",
          "type": "esriFieldTypeString",
          "alias": "ETIQUETA",
          "length": 200
      }, {
          "name": "ESTILO",
          "type": "esriFieldTypeInteger",
          "alias": "ESTILO"
      }, {
          "name": "ANGULO",
          "type": "esriFieldTypeSmallInteger",
          "alias": "ANGULO"
      }, {
          "name": "ALINEACION",
          "type": "esriFieldTypeString",
          "alias": "ALINEACION",
          "length": 50
      }, {
        "name": "TIPO",
        "type": "esriFieldTypeString",
        "alias": "Tipo",
        "length": 50
      }, {
        "name": "DOMINIO",
        "type": "esriFieldTypeSmallInteger",
        "alias": "DOMINIO"
      }, {
          "name": "ESTADO",
          "type": "esriFieldTypeSmallInteger",
          "alias": "ESTADO"
      }, {
          "name": "HOJA",
          "type": "esriFieldTypeString",
          "alias": "HOJA",
          "length": 4
      }, {
          "name": "CUADRANTE",
          "type": "esriFieldTypeString",
          "alias": "CUADRANTE",
          "length": 1
      }, {
          "name": "CODHOJA",
          "type": "esriFieldTypeString",
          "alias": "CODHOJA",
          "length": 5
      }],
}