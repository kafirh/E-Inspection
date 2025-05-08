namespace MachineInspection.Application.DTO
{
    public class InspectionItemDto
    {
        public int itemId { get; set; }
        public string itemName { get; set; }
        public string specification { get; set; }
        public string method { get; set; }
        public string frequency { get; set; }
    }
}
