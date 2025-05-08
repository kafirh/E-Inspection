using MachineInspection.Application.DTO;
using MachineInspection.Domain.IRepositories;

namespace MachineInspection.Application.Service
{
    public class BusinessUnitService
    {
        private readonly IBusinessUnitRepository _buRepo;

        public BusinessUnitService(IBusinessUnitRepository buRepo)
        {
            _buRepo = buRepo;
        }

        public List<BuDto> GetAll()
        {
            var list = _buRepo.GetAll();
            return list.Select(b => new BuDto
            {
                buId = b.buId,
                buName = b.buName,
            }).ToList();
        }
    }
}
