using System.Reflection;
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

        [HttpPost]
        public async Task<IActionResult> Create(MachineCreateViewDto model)
        {
            if (model.MachineCreateDto == null)
            {
                var viewmodel = _machineFacade.PrepareMachineCreateView();
                return View(viewmodel);
            }

            var result = await _machineFacade.CreateMachineAsync(model.MachineCreateDto);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                var viewmodel = _machineFacade.PrepareMachineCreateView();
                return View(viewmodel);
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

        [HttpPost]
        public async Task<IActionResult> AddItem(string machineId, int inspectionId, string machineName)
        {
            if(machineId == null || inspectionId == 0)
            {
                var model = await _machineFacade.PreparemachineAddItemView(machineId, machineName);
                return View(model);
            }
            //bool result = await _machineFacade.AddItemAsync(machineId,inspectionId);
            //if (!result)
            //{
            //    ModelState.AddModelError(string.Empty, "Gagal menyimpan data mesin.");
            //    var model = await _machineFacade.PreparemachineAddItemView(machineId, machineName);
            //    return View(model);
            //}

            return RedirectToAction("Create","Inspection", new { machineId = machineId,inspectionId = inspectionId });
        }
    }
}
