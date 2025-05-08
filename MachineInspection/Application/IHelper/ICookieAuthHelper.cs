using MachineInspection.Domain.Entities;

namespace MachineInspection.Application.IHelper
{
    public interface ICookieAuthHelper
    {
        Task SignInAsync(BusinessUnit bu, string userName, Role role);
        Task SignOutAsync();
    }
}
