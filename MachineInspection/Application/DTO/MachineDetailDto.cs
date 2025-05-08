namespace MachineInspection.Application.DTO
{
    public class MachineDetailDto
    {
        public MachineDto Machine { get; set; }
        public List<InspectionItemDto> ItemDtos { get; set; }

    }
}
