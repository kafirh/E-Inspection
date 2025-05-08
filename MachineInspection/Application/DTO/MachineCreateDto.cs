namespace MachineInspection.Application.DTO
{
    public class MachineCreateDto
    {
        public string MachineId { get; set; }
        public string SectionName { get; set; }
        public string Line { get; set; }
        public string MachineName { get; set; }
        public string MachineNumber { get; set; }
        public string DocumentNo { get; set; }
        public string BuId { get; set; } // Wajib diisi, bisa dari input/manual/claim
    }
}
