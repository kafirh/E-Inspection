namespace MachineInspection.Domain.Entities
{
    public class Result
    {
        public int id { get; set; }
        public int userId {  get; set; }
        public string status { get; set; }
        public DateTime date { get; set; }
        public string machineId { get; set; }
        public string buId { get; set; }
    }
}
