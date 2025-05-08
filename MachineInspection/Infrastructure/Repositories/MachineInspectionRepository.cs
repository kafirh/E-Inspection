using MachineInspection.Domain.Entities;
using MachineInspection.Domain.IRepositories;
using MachineInspection.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MachineInspection.Infrastructure.Repositories
{
    public class MachineInspectionRepository : IMachineInspectionRepository
    {
        private readonly DatabaseContext _dbContext;

        // Constructor menerima DatabaseContext melalui DI
        public MachineInspectionRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<InspectionItem>> GetByMachineId(string machineId)
        {
            var items = new List<InspectionItem>();

            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();

                var query = @"
            SELECT ii.itemId, ii.itemName, ii.specification, ii.method, ii.frequency,
                   ii.number, ii.imageName, ii.isNumber, ii.prasyarat
            FROM Machine_Inspection mi
            JOIN InspectionItems ii ON mi.inspectionId = ii.itemId
            WHERE mi.machineId = @machineId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@machineId", machineId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            items.Add(new InspectionItem
                            {
                                itemId = Convert.ToInt32(reader["itemId"]),
                                itemName = reader["itemName"].ToString(),
                                specification = reader["specification"].ToString(),
                                method = reader["method"].ToString(),
                                frequency = reader["frequency"].ToString(),
                                number = Convert.ToInt32(reader["number"]),
                                imageName = reader["imageName"].ToString(),
                                isNumber = Convert.ToBoolean(reader["isNumber"]),
                                prasyarat = reader["prasyarat"].ToString()
                            });
                        }
                    }
                }
            }

            return items;
        }
    }
}
