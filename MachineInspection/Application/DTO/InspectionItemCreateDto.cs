namespace MachineInspection.Application.DTO
{
    public class InspectionItemCreateDto
    {
        public string itemName { get; set; }
        public string specification { get; set; }
        public string method { get; set; }
        public string frequency { get; set; }
    }
}
