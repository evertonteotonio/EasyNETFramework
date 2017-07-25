
using System.Data.SqlClient;
using System.IO;

namespace ENF.Common
{
    public static class Data
    {
        public static string ConnectionString => "Data Source=.; Initial Catalog=Easydb; Integrated Security=True; MultipleActiveResultSets=True";
        public static void ExcecuteScript()
        {
            string script = File.ReadAllText(@"E:\Project Docs\MX462-PD\MX756_ModMappings1.sql");
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(script);
                command.CommandType = System.Data.CommandType.Text;
                command.ExecuteNonQuery();
            }
        }
    }
}
