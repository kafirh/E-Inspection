using MachineInspection.Domain.Entities;

namespace MachineInspection.Domain.IRepositories
{
    public interface IMachineInspectionRepository
    {
        Task<List<InspectionItem>> GetByMachineId(string machineId);
        Task<List<int>> GetIdByMachineId(string machineId);
        Task CreateMachineInspection(MachineInspectionItem machineInspectionItem);
    }
}
