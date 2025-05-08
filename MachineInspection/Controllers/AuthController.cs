using MachineInspection.Application.DTO;
using MachineInspection.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace MachineInspection.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            bool result = await _authService.LoginAsync(request);
            if (result == false)
                return Unauthorized(new { message = "Login gagal! NIK atau Password salah." });

            return RedirectToAction("Index", "Home");
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Index", "Auth");
        }
    }
}
