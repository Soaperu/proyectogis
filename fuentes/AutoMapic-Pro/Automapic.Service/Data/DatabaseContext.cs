using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Automapic.Service.Data
{
    [DbConfigurationType(typeof(OracleDbConfiguration))]
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=OracleDbContext") => Database.SetInitializer<DatabaseContext>(null);

        public DatabaseContext(string connectionString)
            : base(connectionString) => Database.SetInitializer<DatabaseContext>(null);

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("UGEO1749");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        private string BuildStoredProcedureQuery(string storedProcedureName, params object[] parameters)
        {
            var query = new StringBuilder();

            query.AppendFormat("begin {0}(", storedProcedureName);

            for (var i = 0; i <= (parameters?.Length ?? 0) - 1; i++)
            {
                if (i > 0) query.Append(",");

                if (parameters[i] is OracleParameter parameter)
                    query.AppendFormat(":{0}", parameter.ParameterName);

            }

            query.Append("); end;");
            return query.ToString();
        }


        private string BuildFunctionQuery(string storedProcedureName, params object[] parameters)
        {
            var query = new StringBuilder();

            query.AppendFormat("begin :result := {0}(", storedProcedureName);

            for (var i = 0; i <= (parameters?.Length ?? 0) - 1; i++)
            {
                if (i > 0) query.Append(",");

                if (parameters[i] is OracleParameter parameter)
                    query.AppendFormat(":{0}", parameter.ParameterName);

            }

            query.Append("); end;");
            return query.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public int ExecuteStoreProcedureCommand(string storedProcedureName, params object[] parameterValues)
        {
            return Database.ExecuteSqlCommand(BuildStoredProcedureQuery(storedProcedureName, parameterValues),
                parameterValues);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public IEnumerable<T> StoreProcedureQuery<T>(string storedProcedureName, params object[] parameterValues) where T : class
        {
            return Database.SqlQuery<T>(BuildStoredProcedureQuery(storedProcedureName, parameterValues),
                parameterValues);
        }

        /// <summary>
        /// Execute function Oracle.
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="returnValue"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object ExecuteScalarFunction(string functionName, OracleParameter returnValue, params object[] parameters)
        {
            var connection = Database.Connection;
            var command = (OracleCommand)connection.CreateCommand();
            command.CommandText = functionName;
            command.CommandType = CommandType.StoredProcedure;

            if (!returnValue.Direction.Equals(ParameterDirection.ReturnValue))
                returnValue.Direction = ParameterDirection.ReturnValue;

            // Añadir parametros a Oracle
            command.Parameters.Add(returnValue);

            for (var i = 0; i <= (parameters?.Length ?? 0) - 1; i++)
                if (parameters[i] is OracleParameter parameter)
                    command.Parameters.Add(parameter);

            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
                connection.Open();

            command.ExecuteScalar();
            return command.Parameters[returnValue.ParameterName].Value;
        }

        /// <summary>
        /// Obtiene la secuencia ejecutada en la base de datos.
        /// </summary>
        /// <param name="sequenceName"></param>
        /// <returns></returns>
        public int GetSequence(string sequenceName)
        {
            return Database.SqlQuery<int>($"SELECT {sequenceName}.nextval FROM dual").First();
        }


        /// <summary>
        /// Executa y devuelve un <see cref="DbDataReader"/>
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public IDataReader ExecuteReader(string storedProcedureName, params object[] parameterValues)
        {
            var connection = Database.Connection;
            var command = CreateCommand(connection, parameterValues);
            command.CommandText = storedProcedureName;
            command.CommandType = CommandType.StoredProcedure;

            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
                connection.Open();

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public DbCommand GetStoredProcedureCommand(string storedProcedureName, params object[] parameterValues)
        {
            var connection = Database.Connection;
            var command = CreateCommand(connection, parameterValues);
            command.CommandText = storedProcedureName;
            command.CommandType = CommandType.StoredProcedure;

            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
                connection.Open();

            return command;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DbCommand CreateCommand(DbConnection connection, params object[] parameters)
        {
            var command = connection.CreateCommand();

            if (parameters == null || parameters.Length == 0)
                command.Parameters.Add(GetDefaultRefCursor());

            // Añadir parametros a Oracle
            for (var i = 0; i <= (parameters?.Length ?? 0) - 1; i++)
            {
                if (!(parameters[i] is OracleParameter parameter))
                {
                    command.Parameters.Add(GetDefaultRefCursor());
                    continue;
                }

                command.Parameters.Add(parameter);
            }
            return command;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public OracleParameter GetDefaultRefCursor()
        {
            return new OracleParameter()
            {
                OracleDbType = OracleDbType.RefCursor,
                Direction = ParameterDirection.Output
            };
        }
    }
}
