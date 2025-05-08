using MachineInspection.Domain.Entities;

namespace MachineInspection.Domain.IRepositories
{
    public interface IBusinessUnitRepository
    {
        Task<BusinessUnit> GetBusinessUnitById(string businessUnitId);
        List<BusinessUnit> GetAll();
    }
}
