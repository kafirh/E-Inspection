using MachineInspection.Application.DTO;
using MachineInspection.Application.Facade;
using Microsoft.AspNetCore.Mvc;

namespace MachineInspection.Controllers
{
    public class CheckSheetController : Controller
    {
        private CheckSheetFacade _checkSheetFacade;
        public CheckSheetController(CheckSheetFacade checkSheetFacade)
        {
            _checkSheetFacade = checkSheetFacade;
        }
        public async Task<IActionResult> Index()
        {
            var vm = await _checkSheetFacade.PrepareCheckSheetFilterView();
            return View(vm);
        }

        [HttpPost]
        public IActionResult SelectChecksheet(ChecksheetFilterViewDto model)
        {
            return RedirectToAction("Checksheet", new
            {
                machineId = model.SelectedMachineId,
                month = model.SelectedMonth,
                year = model.SelectedYear
            });
        }

        [HttpGet]
        public async Task<IActionResult> Checksheet (string machineId, int month, int year)
        {
            var vm = await _checkSheetFacade.GetChecksheetAsync(machineId, month, year);
            return View(vm);
        }
    }
}
