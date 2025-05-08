using MachineInspection.Domain.Entities;

namespace MachineInspection.Domain.IRepositories
{
    public interface IInspectionItemRepository
    {
        Task<List<InspectionItem>> GetAll();
        Task Create(InspectionItem item);
        Task<InspectionItem> Update(InspectionItem item);
    }
}
