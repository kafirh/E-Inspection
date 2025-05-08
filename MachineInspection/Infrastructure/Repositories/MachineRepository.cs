using MachineInspection.Domain.Entities;
using MachineInspection.Domain.IRepositories;
using MachineInspection.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MachineInspection.Infrastructure.Repositories
{
    public class MachineRepository : IMachineRepository
    {
        private readonly DatabaseContext _dbContext;

        // Constructor menerima DatabaseContext melalui DI
        public MachineRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Machine>> GetAll(string? buId = null)
        {
            var machines = new List<Machine>();
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();

                var query = "SELECT machineId, sectionName, machineName, line, machineNumber, documentNo, buId FROM Machines";
                if (!string.IsNullOrEmpty(buId))
                {
                    query += " WHERE buId = @BuId";
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
                        machines.Add(new Machine
                        {
                            machineId = reader["machineId"].ToString(),
                            sectionName = reader["sectionName"].ToString(),
                            machineName = reader["machineName"].ToString(),
                            line = reader["line"].ToString(),
                            machineNumber = reader["machineNumber"].ToString(),
                            documentNo = reader["documentNo"].ToString(),
                            buId = reader["buId"].ToString(),
                        });
                    }
                }
            }

            return machines;
        }

        public async Task Create(Machine machine)
        {
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();

                var query = @"INSERT INTO Machines 
                     (machineId, sectionName, machineName, line, machineNumber, documentNo, buId) 
                     VALUES 
                     (@machineId, @sectionName, @machineName, @line, @machineNumber, @documentNo, @buId)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@machineId", machine.machineId);
                    command.Parameters.AddWithValue("@sectionName", machine.sectionName);
                    command.Parameters.AddWithValue("@machineName", machine.machineName);
                    command.Parameters.AddWithValue("@line", machine.line);
                    command.Parameters.AddWithValue("@machineNumber", machine.machineNumber);
                    command.Parameters.AddWithValue("@documentNo", machine.documentNo);
                    command.Parameters.AddWithValue("@buId", machine.buId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<Machine> GetMachineById(string machineId)
        {
            var machine = new Machine();
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT machineId, machineName,machineNumber FROM Machines WHERE machineId = @machineId", connection);
                command.Parameters.AddWithValue("@machineId", machineId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Machine
                        {
                            machineId = reader["machineId"].ToString(),
                            machineName = reader["machineName"].ToString(),
                            machineNumber = reader["machineNumber"].ToString(),
                        };
                    }
                }
            }

            return machine; // user tidak ditemukan
        }
    }
}
