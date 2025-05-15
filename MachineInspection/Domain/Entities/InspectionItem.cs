namespace MachineInspection.Domain.Entities
{
    public class InspectionItem
    {
        public int itemId {  get; set; }
        public string itemName { get; set; }
        public string specification { get; set; }
        public string method { get; set; }
        public string frequency { get; set; }
        public bool isNumber { get; set; }
        public string prasyarat { get; set; }
    }
}
