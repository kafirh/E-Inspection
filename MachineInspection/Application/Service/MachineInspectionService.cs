using MachineInspection.Application.DTO;
using MachineInspection.Domain.IRepositories;

namespace MachineInspection.Application.Service
{
    public class MachineInspectionService
    {
        private readonly IMachineInspectionRepository _machineInspectionRepository;
        public MachineInspectionService(IMachineInspectionRepository machineInspectionRepository)
        {
            _machineInspectionRepository = machineInspectionRepository;
        }

        public async Task<List<InspectionItemDto>> GetItemByMachineAsync(string machineId)
        {
            var items = await _machineInspectionRepository.GetByMachineId(machineId);
            var itemDtos = items.Select(i => new InspectionItemDto
            {
                itemId = i.itemId,
                itemName = i.itemName,
                specification = i.specification,
                frequency = i.frequency,
                method = i.method,
            }).ToList();
            return itemDtos;
        }
    }
}
