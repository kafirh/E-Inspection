using MachineInspection.Application.DTO;
using MachineInspection.Application.Service;

namespace MachineInspection.Application.Facade
{
    public class DetailResultFacade
    {
        private readonly DetailResultService _detailResultService;
        private readonly MachineInspectionService _machineInspectionService;

        public DetailResultFacade(DetailResultService detailResultService, MachineInspectionService machineInspectionService)
        {
            _detailResultService = detailResultService;
            _machineInspectionService = machineInspectionService;
        }

        public async Task Create(string machineId,int resultId)
        {
            List<int> inspectionIds = await _machineInspectionService.GetIdItemByMachineAsync(machineId);
            if (inspectionIds == null || !inspectionIds.Any())
            {
                // Bisa log, lempar exception, atau tampilkan pesan
                //throw new InvalidOperationException("Tidak ada inspection item yang ditemukan untuk mesin ini.");
                // Atau jika ingin diam saja:
                 return;
            }
            var tanggal = DateTime.Now;
            var status = "-";
            var remark = "-";
            foreach (var inspection in inspectionIds)
            {
                var detailResultDto = new DetailResultDto
                {
                    remark = remark,
                    status = status,
                    tanggal = tanggal,
                    resultId = resultId,
                    inspectionId = inspection
                };
                await _detailResultService.Create(detailResultDto);
            }

        }
        public async Task<List<DetailResultWithItemDto>> GetDetailResultWithItemDtos(int resultId)
        {
            return await _detailResultService.GetDetailResultWithItemDtos(resultId);
        }
    }
}
