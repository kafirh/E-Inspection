namespace MachineInspection.Application.DTO
{
    public class DetailResultWithItemDto
    {
        public int Id { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }
        public DateTime Tanggal { get; set; }
        public int ResultId { get; set; }

        // Data dari InspectionItem
        public string ItemName { get; set; }
        public string Specification { get; set; }
        public string Method { get; set; }
        public string Frequency { get; set; }
    }

}
