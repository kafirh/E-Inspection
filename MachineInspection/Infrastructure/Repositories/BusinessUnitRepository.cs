using System.Reflection.PortableExecutable;
using MachineInspection.Domain.Entities;
using MachineInspection.Domain.IRepositories;
using MachineInspection.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MachineInspection.Infrastructure.Repositories
{
    public class BusinessUnitRepository : IBusinessUnitRepository
    {
        private readonly DatabaseContext _dbContext;

        // Constructor menerima DatabaseContext melalui DI
        public BusinessUnitRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<BusinessUnit> GetAll()
        {
            var Bus = new List<BusinessUnit>();
            using (var connection = _dbContext.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT buId, buName FROM BusinessUnit", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Bus.Add(new BusinessUnit
                        {
                            buId = reader["buId"].ToString(),
                            buName = reader["buName"].ToString()
                        });
                    }
                }
            }
            return Bus;
        }

        public async Task<BusinessUnit?> GetBusinessUnitById(string businessUnitId)
        {
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT buId, buName FROM BusinessUnit WHERE buId = @buId", connection);
                command.Parameters.AddWithValue("@buId", businessUnitId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new BusinessUnit
                        {
                            buId = reader["buId"].ToString(),
                            buName = reader["buName"].ToString()
                        };
                    }
                }
            }

            return null; // user tidak ditemukan
        }
    }
}
