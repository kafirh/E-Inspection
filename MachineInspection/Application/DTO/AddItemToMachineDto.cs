namespace MachineInspection.Application.DTO
{
    public class AddItemToMachineDto
    {
        public string MachineId { get; set; }
        public string MachineName { get; set; }
        public List<InspectionItemDto> AvailableItems { get; set; }
    }

}
