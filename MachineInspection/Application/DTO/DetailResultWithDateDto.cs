namespace MachineInspection.Application.DTO
{
    public class DetailResultWithDateDto
    {
        public int Id { get; set; }
        public int InspectionId { get; set; }
        public string Status { get; set; }
        public DateTime ResultDate { get; set; } // dari Result.date
    }

}
