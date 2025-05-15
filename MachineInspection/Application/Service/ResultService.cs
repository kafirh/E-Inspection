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
            List<ResultDto> results;
            if (buId == "ALL")
            {
                results = await _resultRepository.GetAllAsync();  // Ambil semua data
            }
            else
            {
                results = await _resultRepository.GetAllAsync(buId);  // Ambil data berdasarkan BU
            }
            return results;
        }

        public async Task<int> CreateAsync(Result result)
        {
            return await _resultRepository.Create(result);
        }
    }
}
