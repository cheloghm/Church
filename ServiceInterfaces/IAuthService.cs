using Church.DTO;
using Church.Models;

namespace Church.ServiceInterfaces
{
    public interface IAuthService
    {
        Task<User> Register(UserAuthDTO userAuthDto, string roleId);
        Task<string> Login(UserAuthDTO userAuthDto);
        Task<string> GeneratePasswordRecoveryToken(string email);
        Task<bool> ValidatePasswordRecoveryToken(string email, string token);
        Task<bool> ResetPassword(string email, string token, string newPassword);
    }
}
