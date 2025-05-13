using MachineInspection.Application.DTO;
using MachineInspection.Domain.Entities;
using MachineInspection.Domain.IRepositories;

namespace MachineInspection.Application.Service
{
    public class DetailResultService
    {
        private readonly IDetailResultRepository _detailResultRepository;
        public DetailResultService(IDetailResultRepository detailResultRepository)
        {
            _detailResultRepository = detailResultRepository;
        }

        public async Task Create (DetailResultDto detailResultDto)
        {
            var detailResult = new DetailResult
            {
                remark = detailResultDto.remark,
                status = detailResultDto.status,
                tanggal = detailResultDto.tanggal,
                resultId = detailResultDto.resultId,
                inspectionId = detailResultDto.inspectionId,
            };
            await _detailResultRepository.Create(detailResult);
        }

        public async Task<List<DetailResultWithItemDto>> GetDetailResultWithItemDtos(int resultId)
        {
            return await _detailResultRepository.GetAll(resultId);
        }

        public async Task<List<DetailResultWithDateDto>> GetDetailResultWithDateDto(string machineId, int year, int month)
        {
            return await _detailResultRepository.GetByMachineAndMonth(machineId, year, month);
        }
    }
}
