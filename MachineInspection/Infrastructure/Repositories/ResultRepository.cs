using System.Reflection.PortableExecutable;
using MachineInspection.Application.DTO;
using MachineInspection.Domain.Entities;
using MachineInspection.Domain.IRepositories;
using MachineInspection.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MachineInspection.Infrastructure.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private readonly DatabaseContext _dbContext;

        // Constructor menerima DatabaseContext melalui DI
        public ResultRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Create(Result result)
        {
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();

                var query = @"INSERT INTO Result 
                     (userId, status, date, machineId, buId) 
                     OUTPUT INSERTED.id
                     VALUES 
                     (@userId, @status, @date, @machineId, @buId)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", result.userId);
                    command.Parameters.AddWithValue("@status", result.status);
                    command.Parameters.AddWithValue("@date", result.date);
                    command.Parameters.AddWithValue("@machineId", result.machineId);
                    command.Parameters.AddWithValue("@buId", result.buId);

                    var insertedId = await command.ExecuteScalarAsync();
                    return Convert.ToInt32(insertedId);
                }
            }
        }


        public async Task<List<ResultDto>> GetAllAsync(string? buId = null)
        {
            var results = new List<ResultDto>();
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();

                var query = @"
            SELECT r.id, u.username, r.status, r.date, r.machineId, r.buId
            FROM Result r
            JOIN Users u ON r.userId = u.id";
                if (!string.IsNullOrEmpty(buId))
                {
                    query += " WHERE r.buId = @BuId";
                }

                var command = new SqlCommand(query, connection);
                if (!string.IsNullOrEmpty(buId))
                {
                    command.Parameters.AddWithValue("@BuId", buId);
                }

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        results.Add(new ResultDto
                        {
                            resultId = Convert.ToInt32(reader["id"]),
                            username = reader["username"].ToString(),
                            status = reader["status"].ToString(),
                            date = Convert.ToDateTime(reader["date"]),
                            machineId = reader["machineId"].ToString(),
                            buId = reader["buId"].ToString(),
                        });
                    }
                }
            }

            return results;
        }
    }
}
