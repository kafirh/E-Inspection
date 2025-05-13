namespace MachineInspection.Application.DTO
{
    public class InspectionItemCreateViewDto
    {
        public InspectionItemCreateDto Item { get; set; } = new();
        public string? MachineId { get; set; }
        public int InspectionId { get; set; } = 0;
        // Tambahan untuk upload file
        public IFormFile? ImageFile { get; set; }

        public string Mode { get; set; } = "Full"; // "Full", "ImageOnly", "NoImage"
    }
}
