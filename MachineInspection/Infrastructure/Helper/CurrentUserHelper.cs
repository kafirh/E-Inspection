using System.Security.Claims;
using MachineInspection.Application.IHelper;

namespace MachineInspection.Infrastructure.Helper
{
    public class CurrentUserHelper : ICurrentUserHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

        public CurrentUserHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? username => User?.Identity?.Name;

        public string? roleId => GetClaim("roleId");
        public string? roleName => GetClaim("roleName");
        public string? buId => GetClaim("buId");
        public string? buName => GetClaim("buName");
        public string? userId => GetClaim("userId");

        private string? GetClaim(string claimType)
        {
            return User?.FindFirst(claimType)?.Value;
        }
    }
}
