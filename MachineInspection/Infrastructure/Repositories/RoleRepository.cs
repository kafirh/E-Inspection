using MachineInspection.Domain.Entities;
using MachineInspection.Domain.IRepositories;
using MachineInspection.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MachineInspection.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DatabaseContext _dbContext;

        // Constructor menerima DatabaseContext melalui DI
        public RoleRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Role?> GetRoleById(int roleId)
        {
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT roleId, roleName FROM Role WHERE roleId = @roleId", connection);
                command.Parameters.AddWithValue("@roleId", roleId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Role
                        {
                            roleId = Convert.ToInt32(reader["roleId"]),
                            roleName = reader["roleName"].ToString()
                        };
                    }
                }
            }

            return null; // user tidak ditemukan
        }
    }
}
