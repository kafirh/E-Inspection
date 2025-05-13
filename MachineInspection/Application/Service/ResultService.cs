using System.Reflection.PortableExecutable;
using MachineInspection.Application.DTO;
using MachineInspection.Domain.Entities;
using MachineInspection.Domain.IRepositories;
using MachineInspection.Infrastructure.Repositories;

namespace MachineInspection.Application.Service
{
    public class ResultService
    {
        private readonly IResultRepository _resultRepository;
        public ResultService(IResultRepository resultRepository)
        {
            _resultRepository = resultRepository;
        }
        public async Task<List<ResultDto>> GetResultDtosAsync(string buId)
        {
            List<Result> results;
            if (buId == "ALL")
            {
                results = await _resultRepository.GetAllAsync();  // Ambil semua data
            }
            else
            {
                results = await _resultRepository.GetAllAsync(buId);  // Ambil data berdasarkan BU
            }
            var resultDtos = results.Select(r => new ResultDto
            {
                resultId = r.id,
                userId = r.userId,
                status = r.status,
                date = r.date,
                machineId = r.machineId,
                buId = buId,
            }).ToList();

            return resultDtos;
        }

        public async Task<int> CreateAsync(ResultDto resultDto)
        {
            var result = new Result
            {
                userId = resultDto.userId,
                status = resultDto.status,
                date = resultDto.date,
                machineId = resultDto.machineId,
                buId = resultDto.buId,
            };
            return await _resultRepository.Create(result);
        }
    }
}
