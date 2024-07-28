using Microsoft.Data.SqlClient;

namespace AngularNetApi.Models
{
    public class StoredProcedureResult
    {
        public SqlDataReader DataReader { get; set; }
        public Dictionary<string, object> OutputParameters { get; set; }
    }
}
