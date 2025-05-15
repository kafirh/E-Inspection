using MachineInspection.Application.DTO;
using MachineInspection.Domain.Entities;

namespace MachineInspection.Domain.IRepositories
{
    public interface IResultRepository
    {
        Task<List<ResultDto>> GetAllAsync(string? buId = null);
        Task<int> Create(Result result);
    }
}
