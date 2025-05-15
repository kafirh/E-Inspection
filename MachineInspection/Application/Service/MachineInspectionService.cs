using MachineInspection.Application.DTO;
using MachineInspection.Domain.Entities;
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
                imageName = i.imageName,
            }).ToList();
            return itemDtos;
        }
        public async Task<List<int>> GetIdItemByMachineAsync(string machineId)
        {
            return await _machineInspectionRepository.GetIdByMachineId(machineId);
        }

        public async Task<bool> CreateMachineInspectionAsync(string machineId, int inspectionId,string imageName)
        {
            if (inspectionId == 0) 
            {
                Console.WriteLine("inspection Id 0");
                return false;
            }
            var machineInspection = new MachineInspectionItem
            {
                machineId = machineId,
                inspectionId = inspectionId,
                imageName = imageName
            };
            try
            {
                await _machineInspectionRepository.CreateMachineInspection(machineInspection);
                return true;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
