using System.Data;

namespace AngularNetApi.Models
{
    public class StoredProcedureResult
    {
        public DataTable DataTable { get; set; }
        public Dictionary<string, object> OutputParameters { get; set; }

        public StoredProcedureResult()
        {
            DataTable = new DataTable();
            OutputParameters = new Dictionary<string, object>();
        }
    }
}
