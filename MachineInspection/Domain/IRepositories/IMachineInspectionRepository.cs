using MachineInspection.Application.DTO;
using MachineInspection.Domain.Entities;

namespace MachineInspection.Domain.IRepositories
{
    public interface IMachineInspectionRepository
    {
        Task<List<InspectionItemWithImageDto>> GetByMachineId(string machineId);
        Task<List<int>> GetIdByMachineId(string machineId);
        Task CreateMachineInspection(MachineInspectionItem machineInspectionItem);
    }
}
