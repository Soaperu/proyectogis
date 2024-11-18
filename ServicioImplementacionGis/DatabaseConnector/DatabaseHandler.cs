using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DatabaseConnector
{
    public class DatabaseHandler
    {
        private readonly DatabaseConnection _dbConnection;

        public DatabaseHandler()
        {
            _dbConnection = new DatabaseConnection(AppConfig.userName, AppConfig.password);
        }

        public string GetConnectionStringByPackage(string procedureName)
        {
            // Regex para encontrar el paquete completo que empieza con "PACK_" hasta el siguiente "."
            var match = Regex.Match(procedureName, @"PACK_[A-Z_]+");

            if (!match.Success)
            {
                throw new ArgumentException($"No se encontró ningún paquete válido en el nombre del procedimiento: {procedureName}");
            }

            var packageName = match.Value; // Extrae el nombre del paquete completo

            // Busca la base de datos asociada al paquete
            var dbType = DatabasePackageMapper.GetDatabaseByPackage(packageName);

            return dbType switch
            {
                "ORACLE" => _dbConnection.OracleConnectionString(),
                "BDGEOCAT" => _dbConnection.GisConnectionString(),
                _ => throw new ArgumentException($"No se encontró ninguna asignación de base de datos para el paquete: {packageName}")
            };
        }

        //Metodos gestores de las consultas SQL
        public DataTable ExecuteDataTable(string storedProcedure, OracleParameter[] parameters)
        {
            var connectionString = GetConnectionStringByPackage(storedProcedure);
            using (var connection = new OracleConnection(connectionString))//_dbConnection.OracleConnectionString()))
            using (var command = new OracleCommand(storedProcedure, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters);
                // Parámetro de salida (ejemplo para obtener un valor booleano de verificación)
                var resultParam = command.Parameters.Add("VO_CURSOR", OracleDbType.RefCursor);
                resultParam.Direction = ParameterDirection.Output;

                var adapter = new OracleDataAdapter(command);
                var dataTable = new DataTable();
                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    throw new InvalidOperationException("Error al ejecutar el procedimiento...", ex);
                }
                return dataTable;
            }
        }
        public string ExecuteStoredProcedureWithReturnValue(string storedProcedure, OracleParameter[] parameters)
        {
            var connectionString = GetConnectionStringByPackage(storedProcedure);
            using (var connection = new OracleConnection(connectionString))//(_dbConnection.OracleConnectionString()))
            using (var command = new OracleCommand(storedProcedure, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                // Agrega los parámetros proporcionados al comando
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                // Parámetro de salida para capturar el valor de retorno
                var returnValue = new OracleParameter("P_RETORNO", OracleDbType.Varchar2, 50)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(returnValue);
                try
                {
                    // Abre la conexión y ejecute el comando
                    connection.Open();
                    command.ExecuteNonQuery();
                    // Check and return the output value as a string
                    return returnValue.Value != null ? returnValue.Value.ToString() : "0";
                }
                catch (OracleException oracleEx)
                {
                    // Maneja excepciones específicas de Oracle
                    throw new ApplicationException($"Oracle error: {oracleEx.Message}", oracleEx);
                }
                catch (Exception ex)
                {
                    // Maneje excepciones generales
                    throw new ApplicationException("Error al ejecutar el procedimiento almacenado.", ex);
                }
            }
        }
        public string ExecuteScalar(string storedProcedure, OracleParameter[] parameters)
        {
            var connectionString = GetConnectionStringByPackage(storedProcedure);
            using (var connection = new OracleConnection(connectionString))//_dbConnection.OracleConnectionString()))
            using (var command = new OracleCommand(storedProcedure, connection))
            {
                OracleTransaction? transaction = null;
                try
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    // Cargar parámetros en el comando
                    command.Parameters.AddRange(parameters);

                    // Agregar el parámetro de salida
                    var outputParameter = new OracleParameter("VO_VA_RETORNO", OracleDbType.Varchar2, 500)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParameter);

                    // Iniciar la transacción
                    transaction = connection.BeginTransaction();
                    command.Transaction = transaction;

                    // Ejecutar el comando
                    command.ExecuteNonQuery();
                    transaction.Commit();

                    // Retornar el valor del parámetro de salida
                    return outputParameter.Value.ToString();
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    throw new InvalidOperationException("Error al ejecutar el procedimiento almacenado y obtener el valor de retorno.", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        // Metodos que consumen los Paquetes SQL
        public DataTable VerifyUser(string username, string password) // Verifica_usuario
        {
            // Nombre del procedimiento almacenado (este nombre es un ejemplo y debería ajustarse)
            string storedProcedure = DatabaseProcedures.Procedure_VerificaUsuario;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("V_USUARIO", OracleDbType.Varchar2, 50) { Value = username },
                //new OracleParameter("V_NOMBRE", OracleDbType.Varchar2, 50) { Value = password },
            };
            return ExecuteDataTable(storedProcedure, parameters);
        }

        public DataTable GetDataDM(string code) // F_Item_Data_DM
        {
            const string procedureName = DatabaseProcedures.Procedure_ListaRegistroCatastroMinero;
            var connectionString = GetConnectionStringByPackage(procedureName);
            using (var connection = new OracleConnection(connectionString))//_dbConnection.GisConnectionString()))
            using (var command = new OracleCommand(procedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Definir los parámetros
                command.Parameters.Add("v_codigo", OracleDbType.Varchar2, 10).Value = code;
                // Parámetro de salida (ejemplo para obtener un valor booleano de verificación)
                var resultParam = command.Parameters.Add("VO_CURSOR", OracleDbType.RefCursor);
                resultParam.Direction = ParameterDirection.Output;

                var dataTable = new DataTable();
                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                    //using (var adapter = new OracleDataAdapter(command))
                    //{
                    //    adapter.Fill(dataTable);
                    //}
                }
                catch (Exception ex)
                {
                    // Manejo de errores detallado
                    throw new InvalidOperationException("Error al ejecutar el procedimiento almacenado.", ex);
                }

                return dataTable;
            }
        }

        public DataTable GetTableCounter(string tableName, string code) // FT_CONTADOR_IT
        {
            string procedureName = DatabaseProcedures.Procedure_ContadorIT;
            var connectionString = GetConnectionStringByPackage(procedureName);
            using (var connection = new OracleConnection(connectionString))
            using (var command = new OracleCommand(procedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Definir los parámetros
                command.Parameters.Add("V_TABLA", OracleDbType.Varchar2, 20).Value = tableName;
                command.Parameters.Add("V_CODIGO", OracleDbType.Varchar2, 10).Value = code;

                var dataTable = new DataTable();
                try
                {
                    connection.Open();
                    using (var adapter = new OracleDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de errores detallado
                    throw new InvalidOperationException("Error al ejecutar el procedimiento almacenado.", ex);
                }

                return dataTable;
            }
        }

        public DataTable VerifyStatusIT(string tableName, string code) // FT_VERIFICA_ESTADO_IT
        {
            // Nombre del procedimiento almacenado
            string procedureName = DatabaseProcedures.Procedure_VerificaEstadoIT;
            var connectionString = GetConnectionStringByPackage(procedureName);
            using (var connection = new OracleConnection(connectionString))
            using (var command = new OracleCommand(procedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Definir los parámetros para el procedimiento almacenado
                command.Parameters.Add("V_TABLA", OracleDbType.Varchar2, 20).Value = tableName;
                command.Parameters.Add("V_CODIGO", OracleDbType.Varchar2, 15).Value = code;

                // Parámetro de salida de tipo cursor
                var cursorParam = command.Parameters.Add("VO_CURSOR", OracleDbType.RefCursor);
                cursorParam.Direction = ParameterDirection.Output;

                var dataTable = new DataTable();
                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de errores detallado
                    throw new InvalidOperationException("Error al ejecutar el procedimiento P_EG_FORMAT_IT.", ex);
                }

                return dataTable;
            }
        }

        public DataTable GetListaDmHistorico(string codigo, string nombre, string fecha, string tipo) // F_SEL_LISTA_DM_HISTORICO
        {
            // Procedimiento almacenado a ejecutar
            string storedProcedure = DatabaseProcedures.Procedure_DmHistorico;
            // Configurar parámetros
            var parameters = new OracleParameter[]
            {
                new OracleParameter("V_CODIGO", OracleDbType.Varchar2, 12) { Value = codigo },
                new OracleParameter("V_NOMBRE", OracleDbType.Varchar2, 25) { Value = nombre },
                new OracleParameter("V_FECHA", OracleDbType.Varchar2, 25) { Value = tipo },
                new OracleParameter("V_TIPO", OracleDbType.Varchar2, 8) { Value = fecha }
            };

            return ExecuteDataTable(storedProcedure, parameters);
        }

        public DataTable GetFechaDmHistorico(string codigo) // F_SEL_FECHA_DM_HISTORICO
        {
            string storedProcedure = DatabaseProcedures.Procedure_FechaDmHistorico;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("V_CODIGO", OracleDbType.Varchar2, 25) { Value = codigo }
            };

            return ExecuteDataTable(storedProcedure, parameters);
        }
        public string DeleteTechnicalReport(string codigo, string tipoInforme) // FT_ELIMINA_INFORME_TECNICO
        {
            // Procedimiento almacenado a ejecutar
            string storedProcedure = DatabaseProcedures.Procedure_EliminarInformeDm;

            // Configurar parámetros
            var parameters = new OracleParameter[]
            {
                new OracleParameter("V_CODIGO", OracleDbType.Varchar2, codigo, ParameterDirection.Input),
                new OracleParameter("V_EG_FORMAT", OracleDbType.Varchar2, tipoInforme, ParameterDirection.Input)
            };
            return ExecuteStoredProcedureWithReturnValue(storedProcedure, parameters);

        }
        public DataTable GetDescriptionList(string selection, string type) // FT_SEL_LISTA_DESCRIPCION
        {
            // Stored procedure name to be executed
            string storedProcedure = DatabaseProcedures.Procedure_ListaDescripcion;

            // Configure parameters
            var parameters = new OracleParameter[]
            {
                new OracleParameter("V_SELECCION", OracleDbType.Varchar2, 1) { Value = selection },
                new OracleParameter("V_TIPO", OracleDbType.Varchar2, 32) { Value = type }
            };
            return ExecuteDataTable(storedProcedure, parameters);

        }
        public DataTable GetCoordinateList(string code, string date) // F_SEL_LISTA_DM_COORDENADA
        {
            // Stored procedure name to be executed
            string storedProcedure = DatabaseProcedures.Procedure_CoordDm;

            // Configure parameters
            var parameters = new OracleParameter[]
            {
                new OracleParameter("CODIGO", OracleDbType.Varchar2, 25) { Value = code },
                new OracleParameter("FECHA", OracleDbType.Varchar2, 8) { Value = date }
            };
            return ExecuteDataTable(storedProcedure, parameters);
        }

        public string GenerateHistory(string reportCode, string reportType) // FT_GENERA_HISTORICO
        {
            // Stored procedure name to be executed
            string storedProcedure = DatabaseProcedures.Procedure_GeneraHistorico;

            // Configure parameters
            var parameters = new OracleParameter[]
            {
                new OracleParameter("V_CODIGO", OracleDbType.Varchar2, 13) { Value = reportCode },
                new OracleParameter("V_EG_FORMAT", OracleDbType.Varchar2, 2) { Value = reportType }
            };
            return ExecuteStoredProcedureWithReturnValue(storedProcedure, parameters);
        }
        public string GenerateHistoryLibDen(string reportCode, string reportType) // FT_GENERA_HISTORICO
        {
            // Stored procedure name to be executed
            string storedProcedure = DatabaseProcedures.Procedure_GeneraHistoricoLibDen;

            // Configure parameters
            var parameters = new OracleParameter[]
            {
                new OracleParameter("V_CODIGO", OracleDbType.Varchar2, 13) { Value = reportCode },
                new OracleParameter("V_EG_FORMAT", OracleDbType.Varchar2, 2) { Value = reportType }
            };
            return ExecuteStoredProcedureWithReturnValue(storedProcedure, parameters);
        }
        public DataTable GetUserRole(string type, string role) // F_Obtiene_Role_Usuario
        {
            string storedProcedure = DatabaseProcedures.Procedure_RolesUsuarios;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("V_TIPO", OracleDbType.Varchar2, 1) { Value = type },
                new OracleParameter("V_ROL", OracleDbType.Varchar2, 30) { Value = role }
            };
            return ExecuteDataTable(storedProcedure, parameters);
        }
        public DataTable GetMenuByOption(string option) // F_Obtiene_Menu_x_Opcion_1
        {
            string storedProcedure = DatabaseProcedures.Procedure_BotonXmenu;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("V_OPCION_MENU", OracleDbType.Varchar2, 6) { Value = option }
            };

            return ExecuteDataTable(storedProcedure, parameters);
        }
        public DataTable VerifyTotalIntersection(string type, string coordinate, string system, string zone, string layer, string reservationCode) // FT_Int_Total
        {
            string storedProcedure = DatabaseProcedures.Procedure_VerificaIntersectTotal;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("vTipo", OracleDbType.Varchar2, 1) { Value = type },
                new OracleParameter("vCoordenada", OracleDbType.Varchar2, 4000) { Value = coordinate },
                new OracleParameter("vsistema", OracleDbType.Varchar2, 10) { Value = system },
                new OracleParameter("vzona", OracleDbType.Varchar2, 2) { Value = zone },
                new OracleParameter("vlayer", OracleDbType.Varchar2, 50) { Value = layer },
                new OracleParameter("v_codreserva", OracleDbType.Varchar2, 50) { Value = reservationCode }
            };
            return ExecuteDataTable(storedProcedure, parameters);
        }
        public DataTable ExecuteTotalIntersection(string type, string coordinate, string zone, string layer, string reservationCode, string classType) // P_Int_Parcial
        {
            string storedProcedure = DatabaseProcedures.Procedure_VerificaIntersectTotal;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("vTipo", OracleDbType.Varchar2, 1) { Value = type },
                new OracleParameter("vCoordenada", OracleDbType.Varchar2, 4000) { Value = coordinate },
                new OracleParameter("vzona", OracleDbType.Varchar2, 2) { Value = zone },
                new OracleParameter("vlayer", OracleDbType.Varchar2, 50) { Value = layer },
                new OracleParameter("v_codreserva", OracleDbType.Varchar2, 20) { Value = reservationCode },
                new OracleParameter("v_clase", OracleDbType.Varchar2, 20) { Value = classType }
            };

            return ExecuteDataTable(storedProcedure, parameters);
        }
        public DataTable VerifyPartialIntersection(string type, string coordinate, string system, string zone, string layer) // FT_Int_Parcial
        {
            string storedProcedure = DatabaseProcedures.Procedure_VerificaIntersectParcial;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("vTipo", OracleDbType.Varchar2, 1) { Value = type },
                new OracleParameter("vCoordenada", OracleDbType.Varchar2, 4000) { Value = coordinate },
                new OracleParameter("vsistema", OracleDbType.Varchar2, 10) { Value = system },
                new OracleParameter("vzona", OracleDbType.Varchar2, 2) { Value = zone },
                new OracleParameter("vlayer", OracleDbType.Varchar2, 50) { Value = layer }
            };

            return ExecuteDataTable(storedProcedure, parameters);
        }
        public string EvaluateFreeDenunciation(string action, string code, string egFormat, string user, string freeDenunciationDate) // FT_SG_D_Libden_EVALGIS
        {
            string storedProcedure = DatabaseProcedures.Procedure_EvalLibreDenunciabilidad;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("V_ACCION", OracleDbType.Varchar2, 13) { Value = action },
                new OracleParameter("V_CGCODIGO", OracleDbType.Varchar2, 13) { Value = code },
                new OracleParameter("V_EGFORMAT", OracleDbType.Varchar2, 2) { Value = egFormat },
                new OracleParameter("V_USLOGUSE", OracleDbType.Varchar2, 32) { Value = user },
                new OracleParameter("V_FECHA", OracleDbType.Varchar2, 32) { Value = freeDenunciationDate }
            };

            return ExecuteStoredProcedureWithReturnValue(storedProcedure, parameters).ToString();
        }
        public DataTable GetFreeDenunciationDate(string egFormat, string freeDenunciationDate) // FT_Fec_Libden_EVALGIS
        {
            string storedProcedure = DatabaseProcedures.Procedure_VerificaLibreDenunciabilidad;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("V_EGFORMAT", OracleDbType.Varchar2, 2) { Value = egFormat },
                new OracleParameter("V_FECHA", OracleDbType.Varchar2, 32) { Value = freeDenunciationDate }
            };

            return ExecuteDataTable(storedProcedure, parameters);
        }
        public DataTable GetPmaPetitions(string datum)
        {
            string storedProcedure = "PACK_DBA_GIS.P_consulta_sc_d_petpma";
            var parameters = new OracleParameter[]
            {
                new OracleParameter("datum_i", OracleDbType.Varchar2, 8) { Value = datum }
            };

            return ExecuteDataTable(storedProcedure, parameters);
        }
        public DataTable GetDmxPma(string datum)
        {
            string storedProcedure = "PACK_DBA_GIS.P_SEL_DMXPMA";
            var parameters = new OracleParameter[]
            {
                new OracleParameter("datum_i", OracleDbType.Varchar2, 8) { Value = datum }
            };

            return ExecuteDataTable(storedProcedure, parameters);
        }
        public DataTable GetDailyPetitionRecord(string date) // FT_fecsup
        {
            string storedProcedure = DatabaseProcedures.Procedure_PeticionesDiarias;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("V_FECHA", OracleDbType.Varchar2, 32) { Value = date }
            };

            return ExecuteDataTable(storedProcedure, parameters);
        }
        public DataTable GetSuperimposedDMByDay(string reportDate, string startDate, string endDate, string connectionType) // P_SEL_DMSUPERPUESTOXDIA
        {
            string storedProcedure = DatabaseProcedures.Procedure_SuperpuestoxDia;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("FECREPO", OracleDbType.Varchar2, 32) { Value = reportDate },
                new OracleParameter("FECREPO_INICIO", OracleDbType.Varchar2, 32) { Value = startDate },
                new OracleParameter("FECREPO_FIN", OracleDbType.Varchar2, 32) { Value = endDate },
                new OracleParameter("TIPOCON", OracleDbType.Varchar2, 8) { Value = connectionType }
            };

            return ExecuteDataTable(storedProcedure, parameters);
        }

        public DataTable GetDetailedSuperimposedDMByDay(string reportDate, string startDate, string endDate, string connectionType) // P_SEL_DMSUPERPUESTOXDIA_DET
        {
            string storedProcedure = DatabaseProcedures.Procedure_DetalleSuperpuestoxDia;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("FECREPO", OracleDbType.Varchar2, 32) { Value = reportDate },
                new OracleParameter("FECREPO_INICIO", OracleDbType.Varchar2, 32) { Value = startDate },
                new OracleParameter("FECREPO_FIN", OracleDbType.Varchar2, 32) { Value = endDate },
                new OracleParameter("TIPOCON", OracleDbType.Varchar2, 8) { Value = connectionType }
            };

            return ExecuteDataTable(storedProcedure, parameters);
        }

        //
        public string InsertDMEvaluationDetail(string code, string date, string type, string uniqueCode, string concession, string zone, double hectareGis, double intersectArea) // P_INS_DM_DETEVAL
        {
            string storedProcedure = DatabaseProcedures.Procedure_InsertaDMEvaluacion;
            var parameters = new OracleParameter[]
                {
                    new OracleParameter("V_CODIGO", OracleDbType.Varchar2, 13) { Value = code },
                    new OracleParameter("V_FECHA_DM", OracleDbType.Varchar2, 10) { Value = date },
                    new OracleParameter("VTIPO", OracleDbType.Varchar2, 2) { Value = type },
                    new OracleParameter("CG_CODIGO", OracleDbType.Varchar2, 13) { Value = uniqueCode },
                    new OracleParameter("PE_NOMDER", OracleDbType.Varchar2, 50) { Value = concession },
                    new OracleParameter("PE_ZONCAT", OracleDbType.Varchar2, 2) { Value = zone },
                    new OracleParameter("HECTAGIS", OracleDbType.Double, 9) { Value = hectareGis },
                    new OracleParameter("AREASUP", OracleDbType.Double, 9) { Value = intersectArea }
                };

            return ExecuteScalar(storedProcedure, parameters).ToString();
        }

        public DataTable GetTotalFreeDenunciation(string type) // FT_SEL_TOT_LIBRE_DENUN
        {
            string storedProcedure = "PACK_DBA_SIGCATMIN.P_SEL_TOT_LIBRE_DENUN";
            var parameters = new OracleParameter[]
            {
                new OracleParameter("v_tipotot", OracleDbType.Varchar2, 2) { Value = type }
            };

            return ExecuteDataTable(storedProcedure, parameters);
        }
        public DataTable GetFreeDenunciationDM(string datum, string zone) // 
        {
            string storedProcedure = DatabaseProcedures.Procedure_SeleccionDmLibreDenunciabilidad;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("datum", OracleDbType.Varchar2, 8) { Value = datum },
                new OracleParameter("zona", OracleDbType.Varchar2, 8) { Value = zone }
            };

            return ExecuteDataTable(storedProcedure, parameters);
        }

        public string InsertDmLibDen(string code) // FT_INST_DM_LIBDEN
        {
            string storedProcedure = DatabaseProcedures.Procedure_InsertDmLibreDenunciabilidad;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("CG_CODIGO", OracleDbType.Varchar2, 15) { Value = code }
            };

            return ExecuteScalar(storedProcedure, parameters);
        }
        public string DeleteFreeDenunciation() // FT_DEL_LIBREDEN
        {
            string storedProcedure = DatabaseProcedures.Procedure_EliminacionLibreDenunciabilidad;
            var parameters = Array.Empty<OracleParameter>();

            return ExecuteScalar(storedProcedure, parameters);
        }
        public string UpdateCmFreeDenunciation(string datum) // FT_UPD_CM_LIBREDEN
        {
            string storedProcedure = DatabaseProcedures.Procedure_ActualizacionCmLibreDenunciabilidad;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("v_datum", OracleDbType.Varchar2, 10) { Value = datum }
            };

            return ExecuteScalar(storedProcedure, parameters);
        }
        public string UpdateFreeDenunciation(string code) // FT_UPD_LIBREDEN
        {
            string storedProcedure = DatabaseProcedures.Procedure_ActualizacionLibreDenunciabilidad;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("cg_codigo", OracleDbType.Varchar2, 16) { Value = code }
            };

            return ExecuteScalar(storedProcedure, parameters);
        }
        public DataTable GetFreeDenunciationArea(string datum, string zone, string type) // FT_ARE_LIBRE_DENUN
        {
            string storedProcedure = DatabaseProcedures.Procedure_ObtAreaLibreDenunciabilidad;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("datum", OracleDbType.Varchar2, 8) { Value = datum },
                new OracleParameter("zona", OracleDbType.Varchar2, 8) { Value = zone },
                new OracleParameter("tipo", OracleDbType.Varchar2, 8) { Value = type }
            };

            return ExecuteDataTable(storedProcedure, parameters);
        }

        public DataTable GetSimulationData(string group, string groupFinal, string type, string identity) // FT_grusim
        {
            string storedProcedure = DatabaseProcedures.Procedure_SimulacionEvaDM;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("V_GRUPO", OracleDbType.Varchar2, 32) { Value = group },
                new OracleParameter("V_GRUPOF", OracleDbType.Varchar2, 32) { Value = groupFinal },
                new OracleParameter("V_TIPO", OracleDbType.Varchar2, 4) { Value = type },
                new OracleParameter("V_IDENTI", OracleDbType.Varchar2, 20) { Value = identity }
            };

            return ExecuteDataTable(storedProcedure, parameters);
        }
        public string InsertSimulationEvent(string simulationCode) // FT_pesiev
        {
            string storedProcedure = DatabaseProcedures.Procedure_InsertDmSimulado;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("v_codigou", OracleDbType.Varchar2, 100) { Value = simulationCode }
            };
            return ExecuteScalar(storedProcedure, parameters); // Método ExecuteScalar para retorno de un valor             
        }
        public DataTable GetSimulationGroupSquare(string group, string groupFinal, string type, string identity) // FT_cuasim
        {
            string storedProcedure = DatabaseProcedures.Procedure_SeleccGrupoCuaSimulado;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("V_GRUPO", OracleDbType.Varchar2, 32) { Value = group },
                new OracleParameter("V_GRUPOF", OracleDbType.Varchar2, 32) { Value = groupFinal },
                new OracleParameter("V_TIPO", OracleDbType.Varchar2, 2) { Value = type },
                new OracleParameter("V_IDENTI", OracleDbType.Varchar2, 20) { Value = identity }
            };

            return ExecuteDataTable(storedProcedure, parameters);
        }
        public DataTable GetUbigeoData(string code) // P_SEL_UBIGEO_DM
        {
            string storedProcedure = DatabaseProcedures.Procedure_SeleccUbigeoDm;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("CODIGOU", OracleDbType.Varchar2, 20) { Value = code }
            };

            return ExecuteDataTable(storedProcedure, parameters);
        }
        public DataTable GetSimulationGroupByDate(string simulationDate) // FT_DMXGRSIMUL
        {
            string storedProcedure = DatabaseProcedures.Procedure_SeleccDmXGrupoSimulado;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("V_FECHA", OracleDbType.Varchar2, 32) { Value = simulationDate }
            };

            return ExecuteDataTable(storedProcedure, parameters);
        }
        public DataTable GetConcessionBySimulationGroup(string simulationDate) // FT_CONCESIONXGRSIMUL
        {
            string storedProcedure = DatabaseProcedures.Procedure_SeleccConcesionGrupoSimulado;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("V_FECHA", OracleDbType.Varchar2, 32) { Value = simulationDate }
            };

            return ExecuteDataTable(storedProcedure, parameters);
        }
        public string EvaluateGIS(string action, string code, string format, string user) // FT_SG_D_EVALGIS
        {
            string storedProcedure = DatabaseProcedures.Procedure_EvaluacionGIS;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("V_ACCION", OracleDbType.Varchar2, 13) { Value = action },
                new OracleParameter("V_CGCODIGO", OracleDbType.Varchar2, 13) { Value = code },
                new OracleParameter("V_EGFORMAT", OracleDbType.Varchar2, 2) { Value = format },
                new OracleParameter("V_USLOGUSE", OracleDbType.Varchar2, 32) { Value = user }
            };

            return ExecuteScalar(storedProcedure, parameters); // Método ExecuteScalar para retorno de un valor
        }
        public DataTable CalculateDistanceToBorder(string code, string zone, string system) // FT_distancia
        {
            string storedProcedure = DatabaseProcedures.Procedure_CalculoDistancicaDmFrontera;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("v_codigo", OracleDbType.Varchar2, 32) { Value = code },
                new OracleParameter("v_zona", OracleDbType.Varchar2, 2) { Value = zone },
                new OracleParameter("datum_i", OracleDbType.Varchar2, 8) { Value = system }
            };

            return ExecuteDataTable(storedProcedure, parameters);
        }
        public DataTable GetHistoricalCadastralIntersection(string code, string date, string zone, string type) // FT_Int_Catastro_Historico
        {
            string storedProcedure = DatabaseProcedures.Procedure_ConsultaHistorialDm;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("v_codigo", OracleDbType.Varchar2, 32) { Value = code },
                new OracleParameter("v_fecha", OracleDbType.Varchar2, 10) { Value = date },
                new OracleParameter("v_zona", OracleDbType.Varchar2, 2) { Value = zone },
                new OracleParameter("v_tipo", OracleDbType.Varchar2, 2) { Value = type }
            };

            return ExecuteDataTable(storedProcedure, parameters);
        }
        public DataTable IntersectOracleFeatureClass(string type, string layer1, string layer2, string code) // FT_Intersecta_Fclass_Oracle_1
        {
            string storedProcedure = DatabaseProcedures.Procedure_InterseccionCapasCatastrales;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("v_Tipo", OracleDbType.Varchar2, 2) { Value = type },
                new OracleParameter("v_layer_1", OracleDbType.Varchar2, 50) { Value = layer1 },
                new OracleParameter("v_layer_2", OracleDbType.Varchar2, 50) { Value = layer2 },
                new OracleParameter("v_codigo", OracleDbType.Varchar2, 20) { Value = code }
            };

            return ExecuteDataTable(storedProcedure, parameters);
        }
        public DataTable IntersectOracleFeatureLayers(string type, string layer1, string layer2, string code, string system) // FT_Intersecta_Fclass_Oracle_capas
        {
            string storedProcedure = DatabaseProcedures.Procedure_InterseccionCapas;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("v_zona", OracleDbType.Varchar2, 2) { Value = type },
                new OracleParameter("v_layer_1", OracleDbType.Varchar2, 50) { Value = layer1 },
                new OracleParameter("v_layer_2", OracleDbType.Varchar2, 50) { Value = layer2 },
                new OracleParameter("v_archi", OracleDbType.Varchar2, 20) { Value = code },
                new OracleParameter("v_sistema", OracleDbType.Varchar2, 20) { Value = system }
            };

            return ExecuteDataTable(storedProcedure, parameters);
        }
        public string EvaluateGISCharacteristics(string action, string cgCode, string format, string evalCode, string ieCode, string description, string value, string type, string user) // FT_SG_D_CARAC_EVALGIS
        {
            string storedProcedure = DatabaseProcedures.Procedure_CaracteristicasEvaluacionGIS;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("V_ACCION", OracleDbType.Varchar2, 3) { Value = action },
                new OracleParameter("V_CG_CODIGO", OracleDbType.Varchar2, 13) { Value = cgCode },
                new OracleParameter("V_EG_FORMAT", OracleDbType.Varchar2, 2) { Value = format },
                new OracleParameter("V_CG_CODEVA", OracleDbType.Clob, 4000000) { Value = evalCode },
                new OracleParameter("V_IE_CODIGO", OracleDbType.Clob, 4000000) { Value = ieCode },
                new OracleParameter("V_EG_DESCRI", OracleDbType.Clob, 4000000) { Value = description },
                new OracleParameter("V_EG_VALOR", OracleDbType.Clob, 4000000) { Value = value },
                new OracleParameter("V_EG_TIPO", OracleDbType.Clob, 4000000) { Value = type },
                new OracleParameter("V_USLOGUSE", OracleDbType.Varchar2, 32) { Value = user }
            };

            return ExecuteScalar(storedProcedure, parameters);
        }
        public string SaveNetAreaToRegistry(string cgCode, string value, string user, string type) // FT_Guarda_AreaNeta_Padron
        {
            string storedProcedure = DatabaseProcedures.Procedure_ConsultaAreaNetas;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("V_CG_CODIGO", OracleDbType.Varchar2, 13) { Value = cgCode },
                new OracleParameter("V_EG_VALOR", OracleDbType.Varchar2, 32) { Value = value },
                new OracleParameter("V_USLOGUSE", OracleDbType.Varchar2, 32) { Value = user },
                new OracleParameter("V_EG_TIPO", OracleDbType.Varchar2, 32) { Value = type }
            };

            return ExecuteScalar(storedProcedure, parameters);
        }

        //*----------------------*




        ///Metodos aun no migrados




        //*----------------------*

        public string CountRecords(string type, string search) // FT_Cuenta_Registro
        {
            string storedProcedure = DatabaseProcedures.Procedure_CuentaRegistros;
            var parameters = new OracleParameter[]
            {
                new OracleParameter("V_TIPO", OracleDbType.Varchar2, 1) { Value = type },
                new OracleParameter("V_BUSCA", OracleDbType.Varchar2, 13) { Value = search }
            };

            return ExecuteScalar(storedProcedure, parameters);
        }
        //*----------------------*




        ///Metodos aun no migrados




        //*----------------------*

        public DataTable GetUniqueDM(string code, int type) // F_OBTIENE_DM_UNIQUE
        {
            string storedProcedure = "PACK_DBA_SG_D_EVALGIS.P_SEL_DM_UNIQUE";
            var parameters = new OracleParameter[]
            {
                new OracleParameter("V_CODIGO", OracleDbType.Varchar2, 13) { Value = code },
                new OracleParameter("V_TIPO", OracleDbType.Varchar2, 1) { Value = type }
            };

            return ExecuteDataTable(storedProcedure, parameters);
        }

        public DataTable GetDMDataWGS84(string code) // F_Obtiene_Datos_DM_84
        {
            string storedProcedure = "PACK_DBA_SG_D_EVALGIS.P_SEL_DATOS_DM_WGS_84_OFICIAL";
            var parameters = new OracleParameter[]
            {
        new OracleParameter("V_CODIGO", OracleDbType.Varchar2, 13) { Value = code }
            };

            return ExecuteDataTable(storedProcedure, parameters);
        }



    }
}
