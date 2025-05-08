using MachineInspection.Domain.Entities;

namespace MachineInspection.Domain.IRepositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsername(string username);
    }
}
