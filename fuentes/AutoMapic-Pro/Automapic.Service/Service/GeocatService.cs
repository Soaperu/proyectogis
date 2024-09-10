using Automapic.Service.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automapic.Service.Service
{
    public class GeocatService
    {
        public void GetData()
        {
            string connectionString = "Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.102.0.66)(PORT = 1521)) (CONNECT_DATA = (SERVICE_NAME = BDGEOCAT)));User Id=ugeo1749;Password=ugeo1749";
            DataTable dt = new DataTable();

            using (var ctx = new DatabaseContext(connectionString))
            {

                string query = @"SELECT CODIGO, DESCRIPCION, DETALLE FROM (SELECT CODIGO, DESCRIPCION, DETALLE FROM GED_M_LISTA WHERE TIPO = 'GEOCATMIN_DESKTOP' AND INDICADOR_ACTIVO = 'A' ORDER BY DESCRIPCION)";
                var r = ctx.Database.SqlQuery<object>(query).ToList();
            }
        }
    }
}
