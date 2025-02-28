using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sigcatmin.pro.Application.Contracts.Models;
using Sigcatmin.pro.Application.Contracts.Constants;

namespace Sigcatmin.pro.Application.Settings
{
    public class FeatureClassMappings
    {
        public static List<FeatureClassInfo> GetMappings() => new List<FeatureClassInfo>
        {
                 new FeatureClassInfo
            {
                FeatureClassName= FeatureClassConstants.gstrFC_Departamento,
                LayerName = "Departamento",
                VariableName = "pFeatureLayer_depa"
            },
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_Departamento_Z + v_zona_dm,
                LayerName = "Departamento",
                VariableName = "pFeatureLayer_depa"
            },
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_Departamento_WGS + v_zona_dm,
                LayerName = "Departamento",
                VariableName = "pFeatureLayer_depa"
            },
            new FeatureClassInfo
            {
                FeatureClassName = "DESA_GIS.GPO_DEP_GENERAL_WGS18",
                LayerName = "Departamento",
                VariableName = "pFeatureLayer_depa"
            },

            // Provincia
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (_) => FeatureClassConstants.gstrFC_Provincia,
                LayerNameGenerator = (cd_region_sele) => cd_region_sele == "00" ? "Prov_Colindantes" : "Provincia",
                VariableName = "pFeatureLayer_prov"
            },
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_Provincia_Z + v_zona_dm,
                LayerNameGenerator = (cd_region_sele) => cd_region_sele == "00" ? "Prov_Colindantes" : "Provincia",
                VariableName = "pFeatureLayer_prov"
            },
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_Provincia_WGS + v_zona_dm,
                LayerNameGenerator = (cd_region_sele) => cd_region_sele == "00" ? "Prov_Colindantes" : "Provincia",
                VariableName = "pFeatureLayer_prov"
            },

            // Distrito
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_Distrito,
                LayerName = "Distrito",
                VariableName = "pFeatureLayer_dist"
            },
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_Distrito_Z + v_zona_dm,
                LayerName = "Distrito",
                VariableName = "pFeatureLayer_dist"
            },
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_Distrito_WGS + v_zona_dm,
                LayerName = "Distrito",
                VariableName = "pFeatureLayer_dist"
            },

            // Catastro Minero WGS84
            new FeatureClassInfo
            {
                //FeatureClassName = "DATA_GIS.GPO_CMI_CATASTRO_MINERO_WGS_17",
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_CatastroWGS84 + v_zona_dm,
                LayerName = "Catastro",
                VariableName = "pFeatureLayer_cat"
            },
            //new FeatureClassInfo
            //{
            //    FeatureClassName = "DATA_GIS.GPO_CMI_CATASTRO_MINERO_WGS_18",
            //    LayerName = "Catastro",
            //    VariableName = "pFeatureLayer_cat"
            //},
            //new FeatureClassInfo
            //{
            //    FeatureClassName = "DATA_GIS.GPO_CMI_CATASTRO_MINERO_WGS_19",
            //    LayerName = "Catastro",
            //    VariableName = "pFeatureLayer_cat"
            //},

            // Catastro Minero PSAD56
            new FeatureClassInfo
            {
                //FeatureClassName = "DATA_GIS.GPO_CMI_CATASTRO_MINERO_WGS_17",
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_CatastroPSAD56 + v_zona_dm,
                LayerName = "Catastro",
                VariableName = "pFeatureLayer_cat"
            },

            // Caram
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_Caram56 + v_zona_dm,
                LayerName = "Caram",
                VariableName = "pFeatureLayer_caram"
            },
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_Caram84 + v_zona_dm,
                LayerName = "Caram",
                VariableName = "pFeatureLayer_caram"
            },

            // CEGO0891 Catastro Minero H
            new FeatureClassInfo
            {
                FeatureClassName = "CEGO0891.GPO_CATASTRO_MINERO_H17",
                LayerName = "Catastro",
                VariableName = "pFeatureLayer_cat"
            },
            new FeatureClassInfo
            {
                FeatureClassName = "CEGO0891.GPO_CATASTRO_MINERO_H18",
                LayerName = "Catastro",
                VariableName = "pFeatureLayer_cat"
            },
            new FeatureClassInfo
            {
                FeatureClassName = "CEGO0891.GPO_CATASTRO_MINERO_H19",
                LayerName = "Catastro",
                VariableName = "pFeatureLayer_cat"
            },

            // DESA_GIS Catastro Minero
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => "DESA_GIS.GPO_CMI_CATASTRO_MINERO_WGS_" + v_zona_dm,
                LayerName = "Catastro",
                VariableName = "pFeatureLayer_cat"
            },
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => "DESA_GIS.GPO_CMI_CATASTRO_MINERO_" + v_zona_dm,
                LayerName = "Catastro",
                VariableName = "pFeatureLayer_cat"
            },

            // Uso Minero
            new FeatureClassInfo
            {
                FeatureClassName = "DATA_GIS.GPO_AUM_AREA_USO_MINERO_G56",
                LayerName = "DM_Uso_Minero",
                VariableName = "pFeatureLayer_usomin"
            },

            // Actividad Minera
            new FeatureClassInfo
            {
                FeatureClassName = "DATA_GIS.GPO_AAC_AREA_ACT_MINERA_G56",
                LayerName = "DM_Actividad_Minera",
                VariableName = "pFeatureLayer_Actmin"
            },

            // Cuadriculas
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (_) => FeatureClassConstants.gstrFC_Cuadricula,
                LayerName = "Cuadricula Regional",
                VariableName = "pFeatureLayer_cuadriculasR"
            },
            new FeatureClassInfo
            {
                FeatureClassName = "DATA_GIS.GPO_CUA_CUADRICULAS_WGS_17",
                LayerName = "Cuadriculas",
                VariableName = "pfeaturelayer_cuadriculas"
            },
            new FeatureClassInfo
            {
                FeatureClassName = "DATA_GIS.GPO_CUA_CUADRICULAS_WGS_18",
                LayerName = "Cuadriculas",
                VariableName = "pfeaturelayer_cuadriculas"
            },
            new FeatureClassInfo
            {
                FeatureClassName = "DATA_GIS.GPO_CUA_CUADRICULAS_WGS_19",
                LayerName = "Cuadriculas",
                VariableName = "pfeaturelayer_cuadriculas"
            },

            // Ríos
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (_) => FeatureClassConstants.gstrFC_Rios56,
                LayerName = "Drenaje",
                VariableName = "pFeatureLayer_gene"
            },
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (_) => FeatureClassConstants.gstrFC_Rios84,
                LayerName = "Drenaje",
                VariableName = "pFeatureLayer_gene"
            },

            // Carreteras
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (_) => FeatureClassConstants.gstrFC_Carretera56,
                LayerName = "Vías",
                VariableName = "pFeatureLayer_gene"
            },
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (_) => FeatureClassConstants.gstrFC_Carretera84,
                LayerName = "Vías",
                VariableName = "pFeatureLayer_gene"
            },

            // Centros Poblados
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (_) => FeatureClassConstants.gstrFC_CPoblado56,
                LayerName = "Centro Poblado",
                VariableName = "pFeatureLayer_gene"
            },
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (_) => FeatureClassConstants.gstrFC_CPoblado84,
                LayerName = "Centro Poblado",
                VariableName = "pFeatureLayer_gene"
            },

            // Frontera
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_Frontera_WGS,
                LayerName = "Limite Frontera",
                VariableName = "pFeatureLayer_fron"
            },
            new FeatureClassInfo
            {
                FeatureClassName = FeatureClassConstants.gstrFC_Frontera, // PSAD56
                LayerName = "Limite Frontera",
                VariableName = "pFeatureLayer_fron"
            },
            // Concesiones Forestales
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_Cforestal + v_zona_dm,
                LayerName = "Catastro Forestal",
                VariableName = "pFeatureLayer_fores"
            },
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_forestal + v_zona_dm,
                LayerName = "Catastro Forestal",
                VariableName = "pFeatureLayer_fores"
            },

            // Zona Reservada
            new FeatureClassInfo
            {
                //FeatureClassName = "DATA_GIS.GPO_ARE_AREA_RESERVADA_WGS_17",
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_AReservada84 + v_zona_dm,
                LayerName = "Zona Reservada",
                VariableName = "pFeatureLayer_rese"
            },
            //new FeatureClassInfo
            //{
            //    FeatureClassName = "DATA_GIS.GPO_ARE_AREA_RESERVADA_WGS_18",
            //    LayerName = "Zona Reservada",
            //    VariableName = "pFeatureLayer_rese"
            //},
            //new FeatureClassInfo
            //{
            //    FeatureClassName = "DATA_GIS.GPO_ARE_AREA_RESERVADA_WGS_19",
            //    LayerName = "Zona Reservada",
            //    VariableName = "pFeatureLayer_rese"
            //},

            // Zona Urbana
            new FeatureClassInfo
            {
                //FeatureClassName = "DATA_GIS.GPO_ZUR_ZONA_URBANA_WGS_17",
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_ZUrbanaWgs84 + v_zona_dm,
                LayerName = "Zona Urbana",
                VariableName = "pFeatureLayer_urba"
            },
            new FeatureClassInfo
            {
                //FeatureClassName = "DATA_GIS.GPO_ZUR_ZONA_URBANA_WGS_18",
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_ZUrbanaPsad56 + v_zona_dm,
                LayerName = "Zona Urbana",
                VariableName = "pFeatureLayer_urba"
            },
            //new FeatureClassInfo
            //{
            //    FeatureClassName = "DATA_GIS.GPO_ZUR_ZONA_URBANA_WGS_19",
            //    LayerName = "Zona Urbana",
            //    VariableName = "pFeatureLayer_urba"
            //},
            // Hojas IGN
            new FeatureClassInfo
            {
                FeatureClassName = "DATA_GIS.GPO_HOJ_HOJAS_18",
                LayerName = "Carta IGN",
                VariableName = "pFeatureLayer_hoja"
            }
        };

    }
}
