using MachineInspection.Application.DTO;
using MachineInspection.Application.IHelper;
using MachineInspection.Application.Service;
using MachineInspection.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MachineInspection.Application.Facade
{
    public class ResultFacade
    {
        private readonly ResultService _resultService;
        private readonly ICurrentUserHelper _currentUserHelper;
        private readonly MachineService _machineService;
        private readonly DetailResultFacade _detailResultFacade;
        public ResultFacade(ResultService resultService, ICurrentUserHelper currentUserHelper, MachineService machineService,DetailResultFacade detailResultFacade) 
        {
            _resultService = resultService;
            _currentUserHelper = currentUserHelper;
            _machineService = machineService;
            _detailResultFacade = detailResultFacade;
        }
        public async Task<List<ResultDto>> GetResultDtosAsync()
        {
            var buId = _currentUserHelper.buId;
            return await _resultService.GetResultDtosAsync(buId);
        }

        public async Task<bool> CreateResultAsync(string machineId)
        {
            try
            {
                var userId = _currentUserHelper.userId;
                var buId = _currentUserHelper.buId;
                DateTime date = DateTime.Now;
                var status = "-";
                var result = new Result
                {
                    userId = Convert.ToInt32(userId),
                    buId = buId,
                    date = date,
                    status = status,
                    machineId = machineId
                };
                int resultId = await _resultService.CreateAsync(result);
                await _detailResultFacade.Create(machineId, resultId);
                return true;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<ResultCreateViewDto> PrepareResultCreateView()
        {
            var buId = _currentUserHelper.buId;

            var machineDtos = await _machineService.GetMachineDtosAsync(buId);

            var machineList = machineDtos?
                .Select(m => new SelectListItem
                {
                    Value = m.machineId,
                    Text = m.machineName
                })
                .ToList() ?? new List<SelectListItem>();

            return new ResultCreateViewDto
            {
                machineList = machineList
            };
        }

    }
}
