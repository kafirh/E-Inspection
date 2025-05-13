using System.Reflection.PortableExecutable;
using MachineInspection.Application.DTO;
using MachineInspection.Domain.Entities;
using MachineInspection.Domain.IRepositories;
using MachineInspection.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MachineInspection.Infrastructure.Repositories
{
    public class DetailResultRepository : IDetailResultRepository
    {
        private readonly DatabaseContext _dbContext;

        // Constructor menerima DatabaseContext melalui DI
        public DetailResultRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Create(DetailResult detailResult)
        {
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();

                var query = @"INSERT INTO DetailResult 
                     (remark, status, tanggal, resultId, inspectionId) 
                     VALUES 
                     (@remark, @status, @tanggal, @resultId, @inspectionId)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@remark", detailResult.remark);
                    command.Parameters.AddWithValue("@status", detailResult.status);
                    command.Parameters.AddWithValue("@tanggal", detailResult.tanggal);
                    command.Parameters.AddWithValue("@resultId", detailResult.resultId);
                    command.Parameters.AddWithValue("@inspectionId", detailResult.inspectionId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<DetailResultWithItemDto>> GetAll(int resultId)
        {
            var results = new List<DetailResultWithItemDto>();

            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();

                var query = @"
                SELECT 
                    dr.id, dr.remark, dr.status, dr.tanggal, dr.resultId,
                    ii.itemName, ii.specification, ii.method, ii.frequency
                FROM  (SELECT dr.id, dr.remark, dr.status, dr.tanggal, dr.resultId, dr.inspectionId 
                        FROM DetailResult dr WHERE dr.resultId = @resultId) dr
                JOIN InspectionItems ii ON dr.inspectionId = ii.itemId"; // Pindahkan WHERE ke bagian akhir

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@resultId", resultId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            results.Add(new DetailResultWithItemDto
                            {
                                Id = reader.GetInt32(0),
                                Remark = reader.GetString(1),
                                Status = reader.GetString(2),
                                Tanggal = reader.GetDateTime(3),
                                ResultId = reader.GetInt32(4),
                                ItemName = reader.GetString(5),
                                Specification = reader.GetString(6),
                                Method = reader.GetString(7),
                                Frequency = reader.GetString(8)
                            });
                        }
                    }
                }
            }

            return results;
        }

        public async Task<List<DetailResultWithDateDto>> GetByMachineAndMonth(string machineId, int year, int month)
        {
            var results = new List<DetailResultWithDateDto>();
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);

            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();

                var query = @"
            SELECT dr.id, dr.inspectionId, dr.status, r.date
            FROM DetailResult dr
            JOIN Result r ON dr.resultId = r.id
            WHERE r.machineId = @machineId
              AND r.date >= @startDate AND r.date < @endDate";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@machineId", machineId);
                    command.Parameters.AddWithValue("@startDate", startDate);
                    command.Parameters.AddWithValue("@endDate", endDate);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            results.Add(new DetailResultWithDateDto
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                InspectionId = Convert.ToInt32(reader["inspectionId"]),
                                Status = reader["status"].ToString(),
                                ResultDate = Convert.ToDateTime(reader["date"])
                            });
                        }
                    }
                }
            }

            return results;
        }
    }
}
