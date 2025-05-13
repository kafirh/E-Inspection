using MachineInspection.Application.DTO;
using MachineInspection.Domain.Entities;

namespace MachineInspection.Domain.IRepositories
{
    public interface IDetailResultRepository
    {
        Task<List<DetailResultWithItemDto>> GetAll(int DetailResult);
        Task<List<DetailResultWithDateDto>> GetByMachineAndMonth(string machineId, int year, int month);
        Task Create (DetailResult detailResult);
    }
}
