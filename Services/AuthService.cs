using Church.DTO;
using Church.Helpers;
using Church.Models;
using Church.ServiceInterfaces;
using Church.RepositoryInterfaces;
using Microsoft.Extensions.Configuration;

namespace Church.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        private readonly IRoleService _roleService;

        public AuthService(IAuthRepository authRepository, IConfiguration configuration, IRoleService roleService)
        {
            _authRepository = authRepository;
            _configuration = configuration;
            _roleService = roleService;
        }

        public async Task<User> Register(UserAuthDTO userAuthDto, string roleId)
        {
            if (await _authRepository.UserExists(userAuthDto.Email))
                return null;

            var user = new User
            {
                Email = userAuthDto.Email,
                RoleId = roleId
            };

            PasswordHelper.CreatePasswordHash(userAuthDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _authRepository.Register(user);

            return user;
        }

        public async Task<string> Login(UserAuthDTO userAuthDto)
        {
            var user = await _authRepository.GetUserByEmail(userAuthDto.Email);

            if (user == null)
                return null;

            if (!PasswordHelper.VerifyPasswordHash(userAuthDto.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            var roleName = _roleService.GetRoleById(user.RoleId).Result.Name;
            var token = PasswordHelper.GenerateJwtToken(user, _configuration, roleName);
            return token;
        }

        public async Task<string> GeneratePasswordRecoveryToken(string email)
        {
            var user = await _authRepository.GetUserByEmail(email);

            if (user == null)
                return null;

            // Generate a password recovery token and store it in the user's record
            var token = Guid.NewGuid().ToString();
            user.PasswordRecoveryToken = token;
            await _authRepository.UpdateUser(user);

            return token;
        }

        public async Task<bool> ValidatePasswordRecoveryToken(string email, string token)
        {
            var user = await _authRepository.GetUserByEmail(email);

            if (user == null)
                return false;

            return user.PasswordRecoveryToken == token;
        }

        public async Task<bool> ResetPassword(string email, string token, string newPassword)
        {
            var user = await _authRepository.GetUserByEmail(email);

            if (user == null || user.PasswordRecoveryToken != token)
                return false;

            PasswordHelper.CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.PasswordRecoveryToken = null;

            await _authRepository.UpdateUser(user);

            return true;
        }
    }
}
