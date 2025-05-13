using MachineInspection.Domain.Entities;

namespace MachineInspection.Application.DTO
{
    public class CheckSheetViewDto
    {
        public MachineDto Machine { get; set; }
        public List<InspectionItemDto> Items { get; set; }
        public Dictionary<(int InspectionId, int Day), string> StatusMap { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
