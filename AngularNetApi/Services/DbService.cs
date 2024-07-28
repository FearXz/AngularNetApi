using AngularNetApi.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;

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

        public DataTable CreateDataTable<T>(IEnumerable<T> data)
        {
            DataTable table = new DataTable();

            // Ottieni le proprietà pubbliche di T
            PropertyInfo[] properties = typeof(T).GetProperties(
                BindingFlags.Public | BindingFlags.Instance
            );

            // Crea colonne nel DataTable corrispondenti alle proprietà
            foreach (var prop in properties)
            {
                table.Columns.Add(
                    prop.Name,
                    Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType
                );
            }

            // Popola il DataTable con i dati della lista
            foreach (var item in data)
            {
                var values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(values);
            }

            return table;
        }
    }
}
