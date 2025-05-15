using System.Text.RegularExpressions;
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
            bool isNumber = DetectIsNumber(itemCreateDto.specification);
            var prasyarat = ExtractPrerequisite(itemCreateDto.specification);
            try
            {
                var item = new InspectionItem
                {
                    itemName = itemCreateDto.itemName,
                    specification = itemCreateDto.specification,
                    frequency = itemCreateDto.frequency,
                    method = itemCreateDto.method,
                    isNumber = isNumber,
                    prasyarat = prasyarat,
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
            bool isNumber = DetectIsNumber(itemCreateDto.specification);
            var prasyarat = ExtractPrerequisite(itemCreateDto.specification);
            try
            {
                var item = new InspectionItem
                {
                    itemName = itemCreateDto.itemName,
                    specification = itemCreateDto.specification,
                    frequency = itemCreateDto.frequency,
                    method = itemCreateDto.method,
                    isNumber = isNumber,
                    prasyarat = prasyarat,
                };
                return await _inspectionItemRepository.CreateWithId(item);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
        }
        private bool DetectIsNumber(string text)
        {
            if (string.IsNullOrEmpty(text))
                return false;

            return text.Any(c => char.IsDigit(c)) || text.Contains(">") || text.Contains("<") || text.Contains("~");
        }

        private string ExtractPrerequisite(string text)
        {
            if (string.IsNullOrEmpty(text))
                return null;

            var regex = new Regex(@"(\d+(\.\d+)?\s*[~><-]\s*\d+(\.\d+)?)|(<=|>=|<|>)\s*\d+(,\d+)?");
            var match = regex.Match(text);

            if (match.Success)
            {
                string found = match.Value.Trim().Replace(" ", "");
                if (found.Contains("~"))
                    return $"range:{found}";
                return found;
            }

            if (text.ToUpper().Contains("MAX"))
            {
                var maxRegex = new Regex(@"MAX\s*(\d+)");
                var maxMatch = maxRegex.Match(text.ToUpper());
                if (maxMatch.Success)
                    return $"max:{maxMatch.Groups[1].Value}";
            }

            if (text.ToUpper().Contains("MIN"))
            {
                var minRegex = new Regex(@"MIN\s*(\d+)");
                var minMatch = minRegex.Match(text.ToUpper());
                if (minMatch.Success)
                    return $"min:{minMatch.Groups[1].Value}";
            }

            return null;
        }
    }
}
