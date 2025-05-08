using MachineInspection.Domain.Entities;

namespace MachineInspection.Domain.IRepositories
{
    public interface IMachineRepository
    {
        Task<List<Machine>> GetAll(string? buId = null);
        Task Create(Machine machine);
        Task<Machine> GetMachineById(string machineId);
    }
}
