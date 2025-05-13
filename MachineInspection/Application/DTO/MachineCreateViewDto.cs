using Microsoft.AspNetCore.Mvc.Rendering;

namespace MachineInspection.Application.DTO
{
    public class MachineCreateViewDto
    {
        public List<SelectListItem>? BuList { get; set; } // untuk dropdown BU
        public bool IsAdmin { get; set; }
        public string? BuId { get; set; }
        public MachineCreateDto MachineCreateDto { get; set; } = new();
    }
}
