using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigcatmin.pro.Application.Contracts.Constants
{
    public static class FeatureClassConstants
    {
        // Departamentos
        public const string gstrFC_Departamento = "DATA_GIS.GPO_DEP_DEPARTAMENTO";
        public const string gstrFC_Departamento_Z = "DATA_GIS.GPO_DEP_DEPARTAMENTO_";
        public const string gstrFC_Departamento_WGS = "DATA_GIS.GPO_DEP_DEPARTAMENTO_WGS_";

        // Provincias
        public const string gstrFC_Provincia = "DATA_GIS.GPO_PRO_PROVINCIA";
        public const string gstrFC_Provincia_Z = "DATA_GIS.GPO_PRO_PROVINCIA_";
        public const string gstrFC_Provincia_WGS = "DATA_GIS.GPO_PRO_PROVINCIA_WGS_";

        // Distritos
        public const string gstrFC_Distrito = "DATA_GIS.GPO_DIS_DISTRITO";
        public const string gstrFC_Distrito_Z = "DATA_GIS.GPO_DIS_DISTRITO_";
        public const string gstrFC_Distrito_WGS = "DATA_GIS.GPO_DIS_DISTRITO_WGS_";

        // Fronteras
        public const string gstrFC_Frontera = "DATA_GIS.GLI_FRO_FRONTERA";
        public const string gstrFC_Frontera_10 = "DATA_GIS.GLI_FRO_FRONTERA_10K";
        public const string gstrFC_Frontera_25 = "DATA_GIS.GLI_FRO_FRONTERA_25K";
        public const string gstrFC_Frontera_Z = "DATA_GIS.GLI_FRO_FRONTERA_";
        public const string gstrFC_Frontera_WGS = "DATA_GIS.GLI_FRO_FRONTERA_WGS_";

        // Catastro
        public const string gstrFC_CatastroPSAD56 = "DATA_GIS.GPO_CMI_CATASTRO_MINERO_";
        public const string gstrFC_CatastroWGS84 = "DATA_GIS.GPO_CMI_CATASTRO_MINERO_WGS_";

        // Cuadrículas
        public const string gstrFC_Cuadricula = "DATA_GIS.GPO_CUA_CUADRICULAS";
        public const string gstrFC_Cuadricula_Z = "DATA_GIS.GPO_CUA_CUADRICULAS_";

        // Caram
        public const string gstrFC_Caram56 = "DATA_GIS.GPO_CAR_CARAM_"; // PSAD56
        public const string gstrFC_Caram84 = "DATA_GIS.GPO_CAR_CARAM_WGS_"; // WGS84

        // Ríos
        public const string gstrFC_Rios56 = "DATA_GIS.GLI_RIO_RIOS";
        public const string gstrFC_Rios84 = "DATA_GIS.GLI_RIO_RIOS_18";

        // Red Vial - carreteras
        public const string gstrFC_Carretera56 = "DATA_GIS.GLI_VIA_VIAS";
        public const string gstrFC_Carretera84 = "DATA_GIS.GLI_VIA_VIAS_18";

        // Centros Poblados
        public const string gstrFC_CPoblado56 = "DATA_GIS.GPT_CPO_CENTRO_POBLADO";
        public const string gstrFC_CPoblado84 = "DATA_GIS.GPT_CPO_CENTRO_POBLADO_18";

        // Hojas Cartográficas
        public const string gstrFC_HCarta56 = "DATA_GIS.GPO_HOJ_HOJAS"; // PSAD56 18S
        public const string gstrFC_HCarta84 = "DATA_GIS.GPO_HOJ_HOJAS_18"; // WGS84 18S

        // Limite de Hojas
        public const string gstrFC_LHojas = "DATA_GIS.GPO_HOJ_LIMITE_HOJAS";

        // Zona Traslape
        public const string gstrFC_ZTraslape = "DATA_GIS.GPO_ZTR_ZONA_TRASLAPE";

        // Capitales de Distrito
        public const string gstrFC_CDistrito56 = "DATA_GIS.GPO_CDI_CAPITAL_DISTRITO";
        public const string gstrFC_CDistrito84 = "DATA_GIS.GPO_CDI_CAPITAL_DISTRITO_18";

        // Área Reservada
        public const string gstrFC_AReservada56 = "DATA_GIS.GPO_ARE_AREA_RESERVADA_G56";
        public const string gstrFC_AReservada84 = "DATA_GIS.GPO_ARE_AREA_RESERVADA_WGS_";

        // Zona Urbana
        public const string gstrFC_ZUrbanaG56 = "DATA_GIS.GPO_ZUR_ZONA_URBANA_G56"; //GEOPSAD56
        public const string gstrFC_ZUrbanaPsad56 = "DATA_GIS.GPO_ZUR_ZONA_URBANA_"; // PSAD56
        public const string gstrFC_ZUrbanaWgs84 = "DATA_GIS.GPO_ZUR_ZONA_URBANA_WGS_"; // WGS84

        // Predio Rural
        public const string gstrFC_prediorural = "DATA_GIS.GPO_PREDIO_RURAL";

        // Geo Boletín
        public const string gstrFC_GEO_BOLETIN100 = "DATA_GIS.GPO_GEO_GEOLOGIA_100K_BOLETIN";
        public const string gstrFC_GEO_BOLETIN100_G56 = "DATA_GIS.GPO_GEO_GEOLOGIA_100K_BOLETIN_G56";

        // Geo Franja
        public const string gstrFC_GEO_FRANJA100 = "DATA_GIS.GPO_GEO_GEOLOGIA_100K_FRANJA";
        public const string gstrFC_GEO_FRANJA100_G56 = "DATA_GIS.GPO_GEO_GEOLOGIA_100K_FRANJA_G56";
        public const string gstrFC_GEO_FRANJA50 = "DATA_GIS.GPO_GEO_GEOLOGIA_50K_FRANJA";
        public const string gstrFC_GEO_FRANJA50_G56 = "DATA_GIS.GPO_GEO_GEOLOGIA_50K_FRANJA_G56";

        // Boletín
        public const string gstrFC_BOLETIN = "DATA_GIS.GPO_BOLETIN";
        public const string gstrFC_BOLETIN_G56 = "DATA_GIS.GPO_BOLETIN_G56";

        // Forestal
        public const string gstrFC_Cforestal = "DATA_GIS.GPO_CFO_CATASTRO_FORESTAL_W";
        public const string gstrFC_forestal = "DATA_GIS.GPO_SERFOR_CONCESIONES_P";

        // Usuario SDE
        public const string glo_User1_SDE = "DESA_GIS";
        public const string glo_User2_SDE = "DATA_GIS";

        // Otros
        public const string gstrFC_Paises = "DATA_GIS.GPO_PAI_PAISES";
        public const string gstrFC_LimiteZonas = "DATA_GIS.GPO_LZO_LIMITE_ZONAS";
        public const string gstrFC_IgnRaster84 = "DATA_GIS.DS_IGN_CARTA_NACIONAL_2016_W84";
        public const string gstrRT_IngMosaic56 = "DATA_GIS.MD_IGN_CARTA_56";
        public const string gstrRT_IngMosaic84 = "DATA_GIS.MD_IGN_CARTA_84";

    }
}
