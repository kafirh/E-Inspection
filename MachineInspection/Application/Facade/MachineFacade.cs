using MachineInspection.Application.DTO;
using MachineInspection.Application.IHelper;
using MachineInspection.Application.Service;
using MachineInspection.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MachineInspection.Application.Facade
{
    public class MachineFacade
    {
        private readonly ICurrentUserHelper _currentUserHelper;
        private readonly MachineService _machineService;
        private readonly BusinessUnitService _businessUnitService;
        private readonly MachineInspectionService _machineInspectionService;
        private readonly InspectionItemService _inspectionItemService;
        public MachineFacade(ICurrentUserHelper currentUserHelper,MachineService machineService,BusinessUnitService businessUnitService,MachineInspectionService machineInspectionService,InspectionItemService inspectionItemService )
        {
            _currentUserHelper = currentUserHelper;
            _businessUnitService = businessUnitService;
            _machineService = machineService;
            _machineInspectionService = machineInspectionService;
            _inspectionItemService = inspectionItemService;
        }

        // Mengambil data mesin melalui MachineService
        public async Task<List<MachineDto>> GetMachineDtosAsync()
        {
            var buId = _currentUserHelper.buId;
            return await _machineService.GetMachineDtosAsync(buId);
        }
        public async Task<MachineDetailDto> PrepareMachineDetailView (string machineId)
        {
            var itemDtos = await _machineInspectionService.GetItemByMachineAsync(machineId);
            var machine = await _machineService.GetMachineByIdAsync(machineId);
            return new MachineDetailDto
            {
                Machine = machine,
                ItemDtos = itemDtos
            };
        }

        public async Task<AddItemToMachineDto> PreparemachineAddItemView (string machineId,string machineName)
        {
            var itemDtos = await _inspectionItemService.GetInspectionItemDtos();
            var model = new AddItemToMachineDto
            {
                MachineId = machineId,
                MachineName = machineName,
                AvailableItems = itemDtos
            };
            return model;
        }

        // Menyiapkan data untuk tampilan Create Machine
        public MachineCreateViewDto PrepareMachineCreateView()
        {
            // Ambil data dari claims
            var roleId = _currentUserHelper.roleId;
            var buId = _currentUserHelper.buId;
            List<SelectListItem>? buList = null;

            // Tentukan apakah admin
            bool isAdmin = roleId == "0";
            if (isAdmin) 
            {
                buList = _businessUnitService.GetAll()
                    .Select(b => new SelectListItem
                    {
                        Value = b.buId,
                        Text = b.buName
                    }).ToList();
            }
            return new MachineCreateViewDto
            {
                BuList = buList,
                IsAdmin = isAdmin,
                BuId = buId,
                MachineCreateDto = new MachineCreateDto() // Siapkan DTO kosong untuk form
            };
        }
        public async Task<bool> CreateMachineAsync(MachineCreateDto machineCreateDto)
        {
            return await _machineService.CreateMachineAsync(machineCreateDto);
        }
    }
}
