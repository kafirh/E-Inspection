namespace MachineInspection.Domain.Entities
{
    public class DetailResult
    {
        public int id { get; set; }
        public string remark { get; set; }
        public string status { get; set; }
        public DateTime tanggal { get; set; }
        public int resultId { get; set; }
        public int inspectionId { get; set; }
    }
}
