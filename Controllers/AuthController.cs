using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Church.ServiceInterfaces;
using Church.DTO;
using Church.Services;
using AutoMapper;

namespace Church.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IRoleService roleService, IMapper mapper)
        {
            _authService = authService;
            _roleService = roleService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            var memberRole = await _roleService.GetRoleByName("Member");

            if (memberRole == null)
                return BadRequest("Member role does not exist");

            var user = await _authService.Register(registerDto, memberRole.Id);

            if (user == null)
                return BadRequest("email is already taken");

            // Map the user to the UserDTO using AutoMapper
            var userDto = _mapper.Map<UserDTO>(user); // Modify this line

            return Ok(userDto);
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
