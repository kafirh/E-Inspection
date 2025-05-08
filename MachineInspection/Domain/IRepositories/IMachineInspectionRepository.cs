using MachineInspection.Domain.Entities;

namespace MachineInspection.Domain.IRepositories
{
    public interface IMachineInspectionRepository
    {
        Task<List<InspectionItem>> GetByMachineId(string machineId);
    }
}
