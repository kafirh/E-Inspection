namespace MachineInspection.Application.DTO
{
    public class DetailResultDto
    {
        public string remark { get; set; }
        public string status { get; set; }
        public DateTime tanggal { get; set; }
        public int resultId { get; set; }
        public int inspectionId { get; set; }
    }
}
