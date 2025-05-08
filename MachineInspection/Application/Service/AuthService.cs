using MachineInspection.Application.DTO;
using MachineInspection.Application.IHelper;
using MachineInspection.Domain.IRepositories;

namespace MachineInspection.Application.Service
{
    public class AuthService
    {
        private readonly ICookieAuthHelper _cookieAuthHelper;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IBusinessUnitRepository _buRepository;

        public AuthService(ICookieAuthHelper cookieAuthHelper,IUserRepository userRepository,IRoleRepository roleRepository,IBusinessUnitRepository businessUnitRepository)
        {
            _cookieAuthHelper = cookieAuthHelper;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _buRepository = businessUnitRepository;
        }

        public async Task<bool> LoginAsync(LoginRequestDto request)
        {
            // 1. Ambil user berdasarkan NIK
            var user = await _userRepository.GetUserByUsername(request.username);
            if (user == null) return false;

            // 2. Validasi password
            if (!BCrypt.Net.BCrypt.Verify(request.password, user.password)) return false;

            // 3. Ambil Role User
            var role = await _roleRepository.GetRoleById(user.roleId);
            if (role == null) return false;

            var bu = await _buRepository.GetBusinessUnitById(user.buId);
            if (bu == null) return false;

            // 4. Simpan session dengan claims menggunakan CookieAuthService
            await _cookieAuthHelper.SignInAsync(bu, user.username, role);

            return true; // Login berhasil
        }

        public async Task LogoutAsync()
        {
            await _cookieAuthHelper.SignOutAsync();
        }
    }
}
