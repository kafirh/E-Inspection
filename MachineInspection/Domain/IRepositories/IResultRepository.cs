using MachineInspection.Domain.Entities;

namespace MachineInspection.Domain.IRepositories
{
    public interface IResultRepository
    {
        Task<List<Result>> GetAllAsync(string? buId = null);
        Task<int> Create(Result result);
    }
}
