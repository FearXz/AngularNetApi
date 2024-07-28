using AngularNetApi.Models;

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
            var parameters = new Dictionary<string, object>();
            _dbService.AddInputParameter(parameters, "@email", loginPost.Email);
            _dbService.AddInputParameter(parameters, "@password", loginPost.Password);

            StoredProcedureResult response = _dbService.ExecuteStoredProcedure(
                _config.GetConnectionString("AngularNetDb"),
                "dbo.GetUserLogin",
                parameters
            );
            using (response.DataReader)
            {
                if (response.DataReader.Read())
                {
                    return new LoginResponse
                    {
                        IdUtente = response.DataReader.GetGuid(0),
                        Nome = response.DataReader.GetString(1),
                        Cognome = response.DataReader.GetString(2),
                        Email = response.DataReader.GetString(3),
                        Citta = response.DataReader.GetString(4),
                        Cellulare = response.DataReader.GetString(5),
                        Indirizzo = response.DataReader.GetString(6),
                        CAP = response.DataReader.GetString(7),
                        Role = response.DataReader.GetString(8)
                    };
                }
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
