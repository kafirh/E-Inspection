using Microsoft.Data.SqlClient;

namespace MachineInspection.Infrastructure.Data
{
    public class DatabaseContext
    {
        private readonly string _connectionString;

        // Terima connection string melalui konstruktor
        public DatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
