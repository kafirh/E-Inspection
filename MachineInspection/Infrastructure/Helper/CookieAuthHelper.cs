using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using MachineInspection.Application.IHelper;
using MachineInspection.Domain.Entities;

namespace MachineInspection.Infrastructure.Helper
{
    public class CookieAuthHelper : ICookieAuthHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CookieAuthHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SignInAsync(BusinessUnit bu, string userName, Role role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim("roleId", role.roleId.ToString()),              
                new Claim("roleName", role.roleName),
                new Claim("buId", bu.buId ?? string.Empty), 
                new Claim("buName", bu.buName)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = false // User tetap login meskipun browser ditutup
            };

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties);
            }
            else
            {
                throw new InvalidOperationException("HttpContext is not available.");
            }
        }

        public async Task SignOutAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            else
            {
                throw new InvalidOperationException("HttpContext is not available.");
            }
        }
    }
}
