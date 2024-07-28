using AngularNetApi.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AngularNetApi.Services
{
    public class DbService
    {
        // Esegue una stored procedure e restituisce il response
        public StoredProcedureResult ExecuteStoredProcedure(
            string connectionString,
            string storedProcedureName,
            Dictionary<string, object> parameters
        )
        {
            using SqlConnection connection = CreateConnection(connectionString);
            using SqlCommand command = CreateCommand(connection, storedProcedureName, parameters);
            return ExecuteCommand(connection, command);
        }

        // Aggiunge un parametro di input alla lista dei parametri

        public void AddInputParameter(
            Dictionary<string, object> parameters,
            string nome,
            object valore
        )
        {
            parameters[nome] = valore ?? DBNull.Value;
        }

        // Aggiunge un parametro di output alla lista dei parametri

        public void AddOutputParameter(
            Dictionary<string, object> parameters,
            string nome,
            SqlDbType tipo,
            object valore = null
        )
        {
            var parametroOutput = new SqlParameter(nome, tipo)
            {
                Direction = ParameterDirection.Output,
                Value = valore ?? DBNull.Value
            };
            parameters[nome] = parametroOutput;
        }

        // Crea una connessione al database

        private SqlConnection CreateConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

        private SqlCommand CreateCommand(
            SqlConnection connection,
            string storedProcedureName,
            Dictionary<string, object> parameters
        )
        {
            SqlCommand command = new SqlCommand(storedProcedureName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            AddParameters(command, parameters);

            return command;
        }

        // Aggiunge il dizionario dei parametri al command

        private void AddParameters(SqlCommand command, Dictionary<string, object> parameters)
        {
            foreach (var param in parameters)
            {
                if (param.Value is SqlParameter sqlParam)
                {
                    command.Parameters.Add(sqlParam);
                }
                else if (param.Value is DataTable dataTable)
                {
                    SqlParameter structuredParam = new SqlParameter(param.Key, SqlDbType.Structured)
                    {
                        TypeName = dataTable.TableName,
                        Value = dataTable
                    };
                    command.Parameters.Add(structuredParam);
                }
                else
                {
                    command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                }
            }
        }

        // Esegue il command e restituisce il response

        private StoredProcedureResult ExecuteCommand(SqlConnection connection, SqlCommand command)
        {
            connection.Open();
            var response = new StoredProcedureResult
            {
                OutputParameters = new Dictionary<string, object>()
            };
            using (SqlDataReader reader = command.ExecuteReader())
            {
                response.DataTable.Load(reader);
            }

            foreach (SqlParameter param in command.Parameters)
            {
                if (
                    param.Direction == ParameterDirection.Output
                    || param.Direction == ParameterDirection.InputOutput
                )
                {
                    response.OutputParameters[param.ParameterName] = param.Value;
                }
            }
            return response;
        }
    }
}
