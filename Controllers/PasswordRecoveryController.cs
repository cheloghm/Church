using Church.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Church.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PasswordRecoveryController : ControllerBase
    {
        private readonly IAuthService _authService;

        public PasswordRecoveryController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("generateToken")]
        public async Task<IActionResult> GeneratePasswordRecoveryToken(string email)
        {
            var token = await _authService.GeneratePasswordRecoveryToken(email);

            if (token == null)
                return NotFound();

            return Ok(token);
        }

        [HttpPost("validateToken")]
        public async Task<IActionResult> ValidatePasswordRecoveryToken(string email, string token)
        {
            var isValid = await _authService.ValidatePasswordRecoveryToken(email, token);

            if (!isValid)
                return BadRequest("Invalid token");

            return Ok();
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword(string email, string token, string newPassword)
        {
            var isSuccess = await _authService.ResetPassword(email, token, newPassword);

            if (!isSuccess)
                return BadRequest("Failed to reset password");

            return Ok();
        }
    }
}
