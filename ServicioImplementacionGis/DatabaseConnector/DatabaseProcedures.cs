using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnector
{
    internal class DatabaseProcedures
    {
        // Paquetes de base de datos y sus procedimientos
        public const string Package_DATA_GIS = "DATA_GIS.PACK_DBA_GIS";


        // Procedimientos
        public const string Procedure_ListaRegistroCatastroMinero = Package_DATA_GIS + ".P_LISTA_REG_CATA_MINERO";
        public const string Procedure_ContadorIT = "PACK_DBA_SG_D_EVALGIS.P_CONTADOR_IT";
        public const string Procedure_VerificaUsuario = "PACK_DBA_SG_D_EVALGIS.P_VERIFICA_USUARIO";
        public const string Procedure_VerificaEstadoIT = "PACK_DBA_SG_D_EVALGIS.P_EG_FORMAT_IT";
        public const string Procedure_DmHistorico = "SISGEM.PACK_DBA_SG_D_EVALGIS.P_SEL_DM_HISTORICO";
        public const string Procedure_FechaDmHistorico = "SISGEM.PACK_DBA_SG_D_EVALGIS.P_FECHA_DM_HISTORICO";
        public const string Procedure_EliminarInformeDm = "PACK_DBA_SG_D_EVALGIS.P_ELIMINAR_INFORME_DM";
        public const string Procedure_ListaDescripcion = "PACK_DBA_SG_D_EVALGIS.P_SEL_DESCRIPCION";
        public const string Procedure_CoordDm = "SISGEM.PACK_DBA_SG_D_EVALGIS.P_SEL_DM_COORDENADA";
        public const string Procedure_GeneraHistorico = "PACK_DBA_SG_D_EVALGIS.P_GENERAR_HISTORICO";
        public const string Procedure_GeneraHistoricoLibDen = "PACK_DBA_SG_D_EVALGIS.P_GENERAR_HISTORICO_LIBDEN";
        public const string Procedure_RolesUsuarios = Package_DATA_GIS + ".P_ROLES_USERS";
        public const string Procedure_BotonXmenu = "PACK_DBA_SG_D_EVALGIS.P_SEL_BOTON_X_MENU_1";
        public const string Procedure_VerificaIntersectTotal = "PACK_DBA_GIS.P_VERIFICA_INTTOTAL";
        public const string Procedure_EjecutaIntersectTotal = "PACK_DBA_GIS.P_EJECUTA_INT_TOTAL";
        public const string Procedure_VerificaIntersectParcial = "PACK_DBA_GIS.p_Verifica_IntParcial";
        public const string Procedure_EvalLibreDenunciabilidad = "PACK_DBA_SG_D_EVALGIS.P_DATOS_LIBDENU_EVALGIS";
        public const string Procedure_VerificaLibreDenunciabilidad = "PACK_DBA_SG_D_EVALGIS.P_VERIFICA_FEC_LIBDEN";
        public const string Procedure_PeticionesDiarias = "PACK_DBA_GIS.P_Consulta_sc_d_regdiariope";
        public const string Procedure_SuperpuestoxDia = "PACK_DBA_SIGCATMIN.P_SEL_DMSUPERPUESTOXDIA";
        public const string Procedure_DetalleSuperpuestoxDia = "PACK_DBA_SIGCATMIN.P_SEL_DMSUPERPUESTOXDIA_DET";
        public const string Procedure_InsertaDMEvaluacion = "PACK_DBA_SIGCATMIN.P_INS_DM_DETEVAL";
        public const string Procedure_SeleccionDmLibreDenunciabilidad = "PACK_DBA_SIGCATMIN.P_SEL_DM_LIBRE_DENUN";
        public const string Procedure_InsertDmLibreDenunciabilidad = "PACK_DBA_SIGCATMIN.P_INS_DM_LIBDEN";
        public const string Procedure_EliminacionLibreDenunciabilidad = "PACK_DBA_SIGCATMIN.P_DEL_LIBDEN";
        public const string Procedure_ActualizacionCmLibreDenunciabilidad = "PACK_DBA_SIGCATMIN.P_UPD_CMI_LIBRE_DENUN";
        public const string Procedure_ActualizacionLibreDenunciabilidad = "PACK_DBA_SIGCATMIN.P_UPD_LIBRE_DENUN";
        public const string Procedure_ObtAreaLibreDenunciabilidad = "PACK_DBA_SIGCATMIN.P_SEL_ARE_LIBRE_DENUN";
        public const string Procedure_SimulacionEvaDM = "PACK_DBA_SIGCATMIN.P_SEL_DM_SIMUL_EV";
        public const string Procedure_InsertDmSimulado = "PACK_DBA_SG_D_EVALGIS.P_INS_DM_SIMUL_EV";
        public const string Procedure_SeleccGrupoCuaSimulado = "PACK_DBA_SIGCATMIN.P_SEL_GRUPO_CUAD_SIMUL";
        public const string Procedure_SeleccUbigeoDm = "PACK_DBA_SIGCATMIN.P_SEL_UBIGEO_DM";
        public const string Procedure_SeleccDmXGrupoSimulado = "PACK_DBA_SIGCATMIN.P_SEL_DMXGRSIMUL_LD";
        public const string Procedure_SeleccConcesionGrupoSimulado = "PACK_DBA_SIGCATMIN.P_SEL_CONCESIONXGRSIMUL_LD";
        public const string Procedure_EvaluacionGIS = "PACK_DBA_SG_D_EVALGIS.P_SG_D_EVALGIS";
        public const string Procedure_CalculoDistancicaDmFrontera = "PACK_DBA_GIS.P_CAL_DIST_DM_FRONTERA";
        public const string Procedure_ConsultaHistorialDm = "DATA_CAT.PACK_DBA_SIGCATMIN.P_SEL_INT_CATASTRO_HISTORICO";
        public const string Procedure_InterseccionCapasCatastrales = "PACK_DBA_GIS.P_Int_Catastro";
        public const string Procedure_InterseccionCapas = "PACK_DBA_GIS.P_INT_capas";
        public const string Procedure_CaracteristicasEvaluacionGIS = "PACK_DBA_SG_D_EVALGIS.P_SG_D_CARAC_EVALGIS";
        public const string Procedure_ConsultaAreaNetas = "PACK_DBA_SG_D_EVALGIS.P_SEL_DATOS_AREASNETAS";
        //public const string Procedure_ = ;
        public const string Procedure_CuentaRegistros = "PACK_DBA_SG_D_EVALGIS.SP_CUENTA_REGISTRO";
        public const string Procedure_ObtenerrDatum = "PACK_DBA_SG_D_EVALGIS.P_OBTENER_DATUM_DM";
        public const string Procedure_ObtenerrDatosResolucion = "PACK_DBA_SG_D_EVALGIS.P_SEL_DATOS_RESO_DM";
        public const string Procedure_SeleccionListaUbigeo = "PACK_DBA_SG_D_EVALGIS.P_SEL_LISTA_UBIGEO";
        public const string Procedure_ObtieneZonaporUbigeo = "PACK_DBA_SG_D_EVALGIS.P_SEL_UBIGEO_ZONA";
        public const string Procedure_ObtenerDatosdeAreasRestringida = "PACK_DBA_SG_D_EVALGIS.P_CUENTA_AREA_RESERVA";
        public const string Procedure_ObtieneDatoRenuncia = "PACK_DBA_SIGCATMIN.P_SEL_DATOS_DM_RENUNCIA";
        public const string Procedure_ObtieneDatoProGisFiltro = "SISGEM.PACK_DBA_SG_D_ARE_GIS.P_SEL_PROGIS_FILTRO";
        public const string Procedure_ObtieneDetalleReg = "SISGEM.PACK_DBA_SG_D_ARE_GIS.P_SEL_DETALLE_REG";
        public const string Procedure_ObtieneOpcionCBox = "PACK_DBA_SG_D_EVALGIS.P_SEL_OPCIONES_CBOX";
        public const string Procedure_ObtieneFiltroUsuario = "SISGEM.PACK_DBA_SG_D_ARE_GIS.P_SEL_FILTRO_USUARIO";
        public const string Procedure_ObtieneFiltroFeatures = "DATA_CAT.PACK_DBA_GIS_ARE_PROD.P_SEL_FILTRO_FEATURES";
        public const string Procedure_ObtieneNumRegReseProd = "DATA_CAT.PACK_DBA_GIS_ARE_PROD.P_SEL_NUMREG_RESE_PROD";
        public const string Procedure_ObtieneNumRegReseTemp = "DATA_CAT.PACK_DBA_GIS_ARE_PROD.P_SEL_NUMREG_RESE_TEMP";
        public const string Procedure_ObtieneNumRegUrbaProd = "DATA_CAT.PACK_DBA_GIS_ARE_PROD.P_SEL_NUMREG_URBA_PROD";
        public const string Procedure_ObtieneNumRegUrbaTemp = "DATA_CAT.PACK_DBA_GIS_ARE_PROD.P_SEL_NUMREG_URBA_TEMP";
        public const string Procedure_ObtieneReseDifreg = "DATA_CAT.PACK_DBA_GIS_ARE_PROD.P_SEL_RESE_DIFREG";
        public const string Procedure_ObtieneUrbaDifreg = "DATA_CAT.PACK_DBA_GIS_ARE_PROD.P_SEL_URBA_DIFREG";
        public const string Procedure_ObtieneFiltroHistorico = "SISGEM.PACK_DBA_SG_D_ARE_GIS.P_SEL_FILTRO_HISTORICO";
        public const string Procedure_ObtieneConsultaGISFiltro = "DATA_CAT.PACK_DBA_GIS_ARE_PROD.P_SEL_CONSULTAGIS_FILTRO";
        public const string Procedure_ObtieneConsultaDatoBD = "SISGEM.PACK_DBA_SG_D_ARE_GIS.P_SEL_CONSULTA_DATOS_BD";
        public const string Procedure_ObtienePathRese = "SISGEM.PACK_DBA_SG_D_ARE_GIS.P_SEL_PATH_RESE";
        public const string Procedure_ObtienePathUrba = "SISGEM.PACK_DBA_SG_D_ARE_GIS.P_SEL_PATH_URBA";
        public const string Procedure_ObtieneImage = "SISGEM.PACK_DBA_SG_D_ARE_GIS.P_SEL_PATH_IMAGE";
        public const string Procedure_ObtieneTipoExe = "SISGEM.PACK_DBA_SG_D_ARE_GIS.P_SEL_TIPO_EXE";
        public const string Procedure_CuentaRegistrosAC = "PACK_DBA_SG_D_EVALGIS.SP_CUENTA_REGISTRO_AC";
        public const string Procedure_ObtieneDatoDMAcumulacion = "PACK_DBA_SIGCATMIN.P_SEL_DATOS_DM_ACUMULACION";
        public const string Procedure_ObtieneDatoDMDivision = "PACK_DBA_SIGCATMIN.P_SEL_DATOS_DM_DIVISION";

        //public const string Procedure_ = ;
        //public const string Procedure_ = ;
        //public const string Procedure_ = ;
        //public const string Procedure_ = ;
    }
}
