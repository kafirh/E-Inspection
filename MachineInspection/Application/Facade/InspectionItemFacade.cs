using MachineInspection.Application.DTO;
using MachineInspection.Application.IHelper;
using MachineInspection.Application.Service;
using Microsoft.IdentityModel.Tokens;

namespace MachineInspection.Application.Facade
{
    public class InspectionItemFacade
    {
        private readonly InspectionItemService _inspectionItemService;
        private readonly MachineInspectionService _machineInspectionService;
        private readonly IImageHelper   _imageHelper;
        public InspectionItemFacade(InspectionItemService inspectionItemService, MachineInspectionService machineInspectionService,IImageHelper imageHelper)
        {
            _inspectionItemService = inspectionItemService;
            _machineInspectionService = machineInspectionService;
            _imageHelper = imageHelper;
        }

        public async Task<List<InspectionItemDto>> GetInspectionItemDtos()
        {
            return await _inspectionItemService.GetInspectionItemDtos();
        }

        public async Task<bool> CreateInspectionItemDto(InspectionItemCreateDto itemCreateDto, string? machineId, IFormFile formFile)
        {
            try
            {
                if (string.IsNullOrEmpty(machineId))
                {
                    Console.WriteLine("pppp");
                    // Hanya membuat inspection item tanpa relasi mesin
                    await _inspectionItemService.CreateInspectionItemDto(itemCreateDto);
                }
                else
                {
                    Console.WriteLine("masukkk");
                    // Membuat inspection item dan hubungkan dengan mesin
                    var inspectionId = await _inspectionItemService.CreateWithIdInspectionItemDto(itemCreateDto);
                    Console.WriteLine($"{inspectionId}");
                    var imageName = await _imageHelper.SaveImageAsync(formFile ,machineId,inspectionId);
                    Console.WriteLine(imageName);
                    await _machineInspectionService.CreateMachineInspectionAsync(machineId, inspectionId,imageName);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public async Task<bool> AddItemAsync(string machineId,int inspectionId, IFormFile formFile)
        {
            var imageName = await _imageHelper.SaveImageAsync(formFile, machineId, inspectionId);
            Console.WriteLine(imageName);
            return await _machineInspectionService.CreateMachineInspectionAsync(machineId, inspectionId, imageName);
        }
    }
}
