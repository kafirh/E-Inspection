using MachineInspection.Application.DTO;
using MachineInspection.Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MachineInspection.Controllers
{
    [Authorize]
    public class InspectionController : Controller
    {
        private readonly InspectionItemService _itemService;
        public InspectionController(InspectionItemService itemService)
        {
            _itemService = itemService;
        }
        public async Task<IActionResult> Index()
        {
            var itemDtos = await _itemService.GetInspectionItemDtos();
            return View(itemDtos);
        }
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(InspectionItemCreateDto itemCreateDto)
        {
            if (itemCreateDto == null)
                return View(itemCreateDto);

            bool result = await _itemService.CreateInspectionItemDto(itemCreateDto);
            if (!result)
            {
                return View(itemCreateDto);
            }
            return RedirectToAction("Index");
        }
    }
}
