using System.Globalization;
using MachineInspection.Application.DTO;
using MachineInspection.Application.Service;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MachineInspection.Application.Facade
{
    public class CheckSheetFacade
    {
        private readonly MachineFacade _machineFacade;
        private readonly DetailResultService _detailResultService;
        public CheckSheetFacade(MachineFacade machineFacade,DetailResultService detailResultService)
        {
            _machineFacade = machineFacade;
            _detailResultService = detailResultService;
        }
        
        public async Task<ChecksheetFilterViewDto> PrepareCheckSheetFilterView()
        {
            var machines = await _machineFacade.GetMachineDtosAsync();
            var currentYear = DateTime.Now.Year;
            var vm = new ChecksheetFilterViewDto
            {
                MachineOptions = machines.Select(m => new SelectListItem
                {
                    Value = m.machineId,
                    Text = $"{m.machineName} - {m.machineNumber}"
                }).ToList(),

                MonthOptions = Enumerable.Range(1, 12).Select(m => new SelectListItem
                {
                    Value = m.ToString(),
                    Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m)
                }).ToList(),

                YearOptions = Enumerable.Range(currentYear - 5, 6).Reverse().Select(y => new SelectListItem
                {
                    Value = y.ToString(),
                    Text = y.ToString()
                }).ToList()
            };

            return vm;
        }
        public async Task<CheckSheetViewDto> GetChecksheetAsync(string machineId, int month, int year)
        {
            var machineItems = await _machineFacade.PrepareMachineDetailView(machineId);

            var machine = machineItems.Machine;
            var inspections = machineItems.ItemDtos;

            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);

            var detailResults = await _detailResultService.GetDetailResultWithDateDto(machineId, year, month);

            var statusMap = detailResults
    .GroupBy(dr => new { dr.InspectionId, Day = dr.ResultDate.Day+1 })
    .ToDictionary(
        g => (g.Key.InspectionId, g.Key.Day),
        g => g.First().Status
    );

            return new CheckSheetViewDto
            {
                Machine = machine,
                Items = inspections,
                StatusMap = statusMap,
                Month = month,
                Year = year
            };
        }

    }
}
