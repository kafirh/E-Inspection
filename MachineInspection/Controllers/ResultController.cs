using System.Reflection;
using MachineInspection.Application.DTO;
using MachineInspection.Application.Facade;
using Microsoft.AspNetCore.Mvc;

namespace MachineInspection.Controllers
{
    public class ResultController : Controller
    {
        private readonly ResultFacade _resultFacade;
        private readonly DetailResultFacade _detailResultFacade;
        public ResultController(ResultFacade resultFacade,DetailResultFacade detailResultFacade)
        {
            _resultFacade = resultFacade;
            _detailResultFacade = detailResultFacade;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _resultFacade.GetResultDtosAsync();
            return View(model);
        }
        [HttpGet]
        public async Task <IActionResult> Create()
        {
            var model = await _resultFacade.PrepareResultCreateView();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ResultCreateViewDto view)
        {
            if(view.machineId == null)
                return View(view);

            bool result = await _resultFacade.CreateResultAsync(view.machineId);
            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Gagal menyimpan data.");
                return View(view);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int resultId)
        {
            var model = await _detailResultFacade.GetDetailResultWithItemDtos(resultId);
            return View(model);
        }
    }
}
