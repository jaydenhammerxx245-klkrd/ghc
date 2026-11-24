using Microsoft.Data.SqlClient;

namespace HotelManagement.DAL.DATSQL
{
    public class DBConnection
    {
        private readonly string _connectionString =
            @"Server=(localdb)\MSSQLLocalDB;  Database=Crown; Trusted_Connection=True;TrustServerCertificate=True;";

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}

