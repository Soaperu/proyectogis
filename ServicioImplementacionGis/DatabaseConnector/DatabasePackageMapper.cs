using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnector
{
    class DatabasePackageMapper
    {
        public static readonly Dictionary<string, string> PackageToDatabaseMap = new Dictionary<string, string>
        {   
            //Mapeo de paquetes y su base de datos
            {"PACK_DBA_GIS", "BDGEOCAT"},
            {"PACK_DBA_GIS_ARE_PROD", "BDGEOCAT"},
            {"PACK_DBA_GIS_ACUMULACION", "BDGEOCAT"},
            {"PACK_DBA_GIS_UEAS",  "BDGEOCAT"},
            {"PACK_DBA_PORCENTAJE_UBICACION", "BDGEOCAT"},
            {"PACK_DBA_SG_D_ARE_GIS", "ORACLE"},
            {"PACK_DBA_SG_D_EVALGIS", "ORACLE"},
            {"PACK_DBA_SG_D_SIMULT_GIS", "ORACLE"},
            {"PACK_DBA_SG_D_UEAS_GIS", "ORACLE"},
            {"PACK_DBA_SIGCATMIN", "BDGEOCAT"},
            {"PACK_DBA_SV_D_PAGOSVIGENCIA", "ORACLE"},
            {"PACK_DBA_UEA_SIGCATMIN", "BDGEOCAT"},
            {"PACK_WEB_LIBRE_DENU", "ORACLE"},
        };

        public static string GetDatabaseByPackage(string packageName)
        {
            return PackageToDatabaseMap.TryGetValue(packageName, out var dbName) ? dbName : null; ;
        }
    }
}
