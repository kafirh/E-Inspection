namespace MachineInspection.Application.DTO
{
    public class ResultDto
    {
        public int resultId { get; set; }
        public string username { get; set; }
        public string status { get; set; }
        public DateTime date { get; set; }
        public string machineId { get; set; }
        public string buId { get; set; } 
    }
}
