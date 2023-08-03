using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Church.ServiceInterfaces;
using Church.DTO;
using Church.Services;

namespace Church.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IRoleService _roleService;

        public AuthController(IAuthService authService, IRoleService roleService)
        {
            _authService = authService;
            _roleService = roleService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserAuthDTO userAuthDto)
        {
            var memberRole = await _roleService.GetRoleByName("Member");

            if (memberRole == null)
                return BadRequest("Member role does not exist");

            var user = await _authService.Register(userAuthDto, memberRole.Id);

            if (user == null)
                return BadRequest("Username is already taken");

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserAuthDTO userAuthDto)
        {
            var token = await _authService.Login(userAuthDto);

            if (token == null)
                return Unauthorized();

            return Ok(new { token });
        }
    }
}
