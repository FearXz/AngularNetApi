using System.Data;
using AngularNetApi.Models;
using Microsoft.Data.SqlClient;

namespace AngularNetApi.Services.DbServices
{
    public class UserService
    {
        private readonly IConfiguration _config;
        private readonly DbService _dbService;

        public UserService(IConfiguration configuration, DbService dbService)
        {
            _config = configuration;
            _dbService = dbService;
        }

        public LoginResponse GetUserByLoginPost(LoginPost loginPost)
        {
            var parameters = new List<SqlParameter>();
            _dbService.AddInputParameter(parameters, "@email", loginPost.Email);
            _dbService.AddInputParameter(parameters, "@password", loginPost.Password);

            StoredProcedureResult response = _dbService.ExecuteStoredProcedure(
                _config.GetConnectionString("AngularNetDb"),
                "dbo.GetUserLogin",
                parameters
            );
            if (response.DataTable.Rows.Count > 0)
            {
                DataRow row = response.DataTable.Rows[0];
                return new LoginResponse
                {
                    IdUtente = (Guid)row["IdUtente"],
                    Nome = (string)row["Nome"],
                    Cognome = (string)row["Cognome"],
                    Email = (string)row["Email"],
                    Citta = (string)row["Citta"],
                    Cellulare = (string)row["Cellulare"],
                    Indirizzo = (string)row["Indirizzo"],
                    CAP = (string)row["CAP"],
                    Role = (string)row["Role"]
                };
            }
            return null;
        }

        //public LoginResponse GetUserByLoginPost(LoginPost loginPost)
        //{
        //    string? con = _config.GetConnectionString("AngularNetDb");

        //    using var conn = new SqlConnection(con);
        //    using var cmd = new SqlCommand("dbo.GetUserLogin", conn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddRange(
        //        [
        //            new SqlParameter("@email", loginPost.Email),
        //            new SqlParameter("@password", loginPost.Password)
        //        ]
        //    );
        //    conn.Open();
        //    using var reader = cmd.ExecuteReader();

        //    if (reader.Read())
        //    {
        //        return new LoginResponse
        //        {
        //            IdUtente = reader.GetGuid(0),
        //            Nome = reader.GetString(1),
        //            Cognome = reader.GetString(2),
        //            Email = reader.GetString(3),
        //            Citta = reader.GetString(4),
        //            Cellulare = reader.GetString(5),
        //            Indirizzo = reader.GetString(6),
        //            CAP = reader.GetString(7),
        //            Role = reader.GetString(8)
        //        };
        //    }
        //    return null;
        //}
    }
}
