namespace MachineInspection.Domain.Entities
{
    public class MachineInspectionItem
    {
        public int machineInspectionId { get; set; }
        public string machineId { get; set; }
        public int inspectionId { get; set; }
        public string imageName { get; set; }
    }
}
