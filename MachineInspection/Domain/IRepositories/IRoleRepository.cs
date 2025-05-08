using MachineInspection.Domain.Entities;

namespace MachineInspection.Domain.IRepositories
{
    public interface IRoleRepository
    {
        Task<Role> GetRoleById(int roleId);
    }
}
