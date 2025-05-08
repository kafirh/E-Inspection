using MachineInspection.Domain.Entities;
using MachineInspection.Domain.IRepositories;
using MachineInspection.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MachineInspection.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _dbContext;

        // Constructor menerima DatabaseContext melalui DI
        public UserRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User?> GetUserByUsername(string username)
        {
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT username, password, buId, roleId FROM Users WHERE username = @username", connection);
                command.Parameters.AddWithValue("@username", username);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new User
                        {
                            username = reader["username"].ToString(),
                            password = reader["password"].ToString(),
                            buId = reader["buId"].ToString(),
                            roleId = Convert.ToInt32(reader["roleId"]),
                        };
                    }
                }
            }

            return null; // user tidak ditemukan
        }
    }
}
