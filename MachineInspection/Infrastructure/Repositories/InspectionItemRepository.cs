using System.Reflection.PortableExecutable;
using MachineInspection.Domain.Entities;
using MachineInspection.Domain.IRepositories;
using MachineInspection.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MachineInspection.Infrastructure.Repositories
{
    public class InspectionItemRepository : IInspectionItemRepository
    {
        private readonly DatabaseContext _dbContext;

        // Constructor menerima DatabaseContext melalui DI
        public InspectionItemRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(InspectionItem item)
        {
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();

                var query = @"INSERT INTO InspectionItems 
                     (itemName, specification, method, frequency,  isNumber,prasyarat) 
                     VALUES 
                     (@itemName, @specification, @method, @frequency, @isNumber,@prasyarat)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@itemName", item.itemName);
                    command.Parameters.AddWithValue("@specification", item.specification);
                    command.Parameters.AddWithValue("@method", item.method);
                    command.Parameters.AddWithValue("@frequency", item.frequency);
                    command.Parameters.AddWithValue("@isNumber", item.isNumber);
                    command.Parameters.AddWithValue("@prasyarat", item.prasyarat);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<InspectionItem>> GetAll()
        {
            var items = new List<InspectionItem>();
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT itemId, itemName, specification, method, frequency, isNumber, prasyarat FROM InspectionItems", connection);

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
                            isNumber = Convert.ToBoolean(reader["isNumber"]),
                            prasyarat = reader["prasyarat"].ToString()
                        });
                    }
                }
            }
            return items;
        }

        public Task<InspectionItem> Update(InspectionItem item)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateWithId(InspectionItem item)
        {
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();

                var query = @"
            INSERT INTO InspectionItems 
                (itemName, specification, method, frequency, isNumber, prasyarat) 
            OUTPUT INSERTED.itemId
            VALUES 
                (@itemName, @specification, @method, @frequency, @isNumber, @prasyarat);";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@itemName", item.itemName);
                    command.Parameters.AddWithValue("@specification", item.specification);
                    command.Parameters.AddWithValue("@method", item.method);
                    command.Parameters.AddWithValue("@frequency", item.frequency);
                    command.Parameters.AddWithValue("@isNumber", item.isNumber);
                    command.Parameters.AddWithValue("@prasyarat", item.prasyarat);

                    // ExecuteScalarAsync untuk mengambil ID hasil insert
                    var result = await command.ExecuteScalarAsync();
                    return Convert.ToInt32(result);
                }
            }
        }

    }
}
