using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ArcGIS.Core.Data.UtilityNetwork.Trace;
using Oracle.ManagedDataAccess.Client;

namespace Automapic_pro.modulos
{
    public class accesoDB
    {
        //private string connectionString;
        static string connectionString = "Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.102.0.66)(PORT = 1521)) (CONNECT_DATA = (SERVICE_NAME = BDGEOCAT)));User Id=bdtecnica;Password=bdtecnica;";
        public static DataTable ConsultaDB(string filter, string fields = "*", string table= "GED_M_LISTA", string type= "GEOCATMIN_DESKTOP")
        {
            // Creamos un dataset y la conexion 
            DataSet ds = new DataSet();
            OracleConnection con = new OracleConnection(connectionString);
            try
            {
                // Abrir la conexión
                con.Open();
                //string pre_query = "SELECT DESCRIPCION  FROM GED_M_LISTA WHERE TIPO = 'GEOCATMIN_DESKTOP' AND INDICADOR_ACTIVO = 'A' AND DETALLE= '{0}' ORDER BY DESCRIPCION";
                string pre_query = "SELECT {0}  FROM {1} WHERE TIPO = '{2}' AND INDICADOR_ACTIVO = 'A' AND DETALLE= '{3}' ORDER BY DESCRIPCION";
                // Crear un comando para ejecutar la consulta SQL
                string query =String.Format(pre_query, fields, table, type, filter);
                OracleCommand cmd = new OracleCommand(query, con);
                cmd.CommandType = CommandType.Text;
                // Crear un adaptador de datos para llenar un DataTable
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                da.Fill(ds);
                //con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en base de datos:\n\n" +ex.Message);
            }
            finally
            {
                // Cerrar la conexión si está abierta
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            // Verificar si el dataset contiene tablas y la tabla tiene filas
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                // Devolver un DataTable vacío o manejar de otra manera
                return new DataTable();
            }
        }
        public static DataTable ConsultaGIS(string fields, string table, string filter="1=1")
        {
            DataSet ds = new DataSet();
            OracleConnection con = new OracleConnection(connectionString);
            try
            {
                con.Open();
                string pre_query = "SELECT {0} FROM {1} WHERE {2}";
                string query = String.Format(pre_query, fields, table, filter);
                OracleCommand cmd = new OracleCommand(query, con);
                cmd.CommandType = CommandType.Text;
                // Crear un adaptador de datos para llenar un DataTable
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                da.Fill(ds);
                //con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error en base de datos espacial:\n\n" + ex.Message);
            }
            finally
            {
                // Cerrar la conexión si está abierta
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            // Verificar si el dataset contiene tablas y la tabla tiene filas
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                // Devolver un DataTable vacío o manejar de otra manera
                return new DataTable();
            }
        }
    }
}
