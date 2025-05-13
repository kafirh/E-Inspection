using Microsoft.AspNetCore.Mvc.Rendering;

namespace MachineInspection.Application.DTO
{
    public class ChecksheetFilterViewDto
    {
        public string SelectedMachineId { get; set; }
        public int SelectedMonth { get; set; }
        public int SelectedYear { get; set; }

        public List<SelectListItem> MachineOptions { get; set; }
        public List<SelectListItem> MonthOptions { get; set; }
        public List<SelectListItem> YearOptions { get; set; }
    }
}
