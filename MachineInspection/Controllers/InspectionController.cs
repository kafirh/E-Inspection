using MachineInspection.Application.DTO;
using MachineInspection.Application.Facade;
using MachineInspection.Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MachineInspection.Controllers
{
    [Authorize]
    public class InspectionController : Controller
    {
        private readonly InspectionItemFacade _inspectionItemFacade;
        public InspectionController(InspectionItemFacade inspectionItemFacade)
        {
            _inspectionItemFacade = inspectionItemFacade;
        }
        public async Task<IActionResult> Index()
        {
            var itemDtos = await _inspectionItemFacade.GetInspectionItemDtos();
            return View(itemDtos);
        }
        [HttpGet]
        public IActionResult Create(string? machineId, int inspectionId = 0) 
        {
            string mode = "Full";
            if (inspectionId != 0) 
            {
                mode = "ImageOnly";
            }
            if (machineId == null) 
            {
                mode = "NoImage";
            }
            var vm = new InspectionItemCreateViewDto { MachineId = machineId, Mode = mode, InspectionId = inspectionId };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(InspectionItemCreateViewDto viewDto)
        {
            if (viewDto.Item == null && viewDto.InspectionId == 0)
                return View(viewDto);
            bool result;
            if (viewDto.InspectionId != 0)
            {
                 result = await _inspectionItemFacade.AddItemAsync(viewDto.MachineId, viewDto.InspectionId, viewDto.ImageFile);
            }
            else
            {
                 result = await _inspectionItemFacade.CreateInspectionItemDto(viewDto.Item, viewDto.MachineId, viewDto.ImageFile);
            }

            if (!result)
            {
                // Tambahkan pesan error jika perlu
                ModelState.AddModelError("", "Gagal menyimpan data.");
                return View(viewDto);
            }

            if (string.IsNullOrEmpty(viewDto.MachineId))
            {
                return RedirectToAction("Index"); // Redirect ke Inspection index
            }

            // Redirect ke controller Machine, action Detail, sambil membawa machineId
            return RedirectToAction("Detail", "Machine", new { machineId = viewDto.MachineId });
        }

    }
}
