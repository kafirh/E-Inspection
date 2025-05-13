using MachineInspection.Application.DTO;
using MachineInspection.Domain.Entities;
using MachineInspection.Domain.IRepositories;

namespace MachineInspection.Application.Service
{
    public class InspectionItemService
    {
        private readonly IInspectionItemRepository _inspectionItemRepository;
        public InspectionItemService(IInspectionItemRepository inspectionItemRepository)
        {
            _inspectionItemRepository = inspectionItemRepository;
        }

        public async Task<List<InspectionItemDto>> GetInspectionItemDtos()
        {
            List<InspectionItem> items = await _inspectionItemRepository.GetAll();
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
        public async Task CreateInspectionItemDto(InspectionItemCreateDto itemCreateDto)
        {
            try
            {
                var item = new InspectionItem
                {
                    itemName = itemCreateDto.itemName,
                    specification = itemCreateDto.specification,
                    frequency = itemCreateDto.frequency,
                    method = itemCreateDto.method,
                    number = itemCreateDto.number,
                    isNumber = itemCreateDto.isNumber,
                    prasyarat = itemCreateDto.prasyarat,
                };
                await _inspectionItemRepository.Create(item);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public async Task<int> CreateWithIdInspectionItemDto(InspectionItemCreateDto itemCreateDto)
        {
            try
            {
                var item = new InspectionItem
                {
                    itemName = itemCreateDto.itemName,
                    specification = itemCreateDto.specification,
                    frequency = itemCreateDto.frequency,
                    method = itemCreateDto.method,
                    number = itemCreateDto.number,
                    isNumber = itemCreateDto.isNumber,
                    prasyarat = itemCreateDto.prasyarat,
                };
                return await _inspectionItemRepository.CreateWithId(item);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
        }
    }
}
