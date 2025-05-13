using Microsoft.AspNetCore.Mvc.Rendering;

namespace MachineInspection.Application.DTO
{
    public class ResultCreateViewDto
    {
        public List<SelectListItem>? machineList {  get; set; }
        public string? machineId { get; set; }
    }
}
