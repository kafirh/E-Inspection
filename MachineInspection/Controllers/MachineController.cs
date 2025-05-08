using MachineInspection.Application.DTO;
using MachineInspection.Application.Facade;
using MachineInspection.Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MachineInspection.Controllers
{
    [Authorize]
    public class MachineController : Controller
    {
        private readonly MachineFacade _machineFacade;

        public MachineController(MachineFacade machineFacade)
        {
            _machineFacade = machineFacade;
        }
        public async Task<IActionResult> Index()
        {
            var machineDtos = await _machineFacade.GetMachineDtosAsync();
            return View(machineDtos);  // Kirim data ke View
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = _machineFacade.PrepareMachineCreateView();
            return View(model);
        }

        // POST (pakai attribute [HttpPost])
        [HttpPost]
        public async Task<IActionResult> Create(MachineCreateViewDto model)
        {
            if (model.MachineCreateDto == null)
                return View(model);

            bool result = await _machineFacade.CreateMachineAsync(model.MachineCreateDto);

            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Gagal menyimpan data mesin.");
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string machineId)
        {
            var model = await _machineFacade.PrepareMachineDetailView(machineId);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddItem(string machineId, string machineName)
        {
            var model = await _machineFacade.PreparemachineAddItemView(machineId, machineName);
            return View(model);
        }
    }
}
