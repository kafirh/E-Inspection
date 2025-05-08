namespace MachineInspection.Domain.Entities
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string buId { get; set; }
        public int roleId { get; set; }
    }
}
