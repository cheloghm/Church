using Church.DTO;
using Church.Models;
using Church.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Church.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Admin,Pastor,Deacon")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            // You can now use userId to filter or perform actions based on the authenticated user

            var userDtos = await _userService.GetAllUsers();
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDetails(string id)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            // You can now use userId to filter or perform actions based on the authenticated user

            var userDto = await _userService.GetUserDetails(id);

            if (userDto == null)
                return NotFound();

            return Ok(userDto);
        }

        [Authorize(Roles = "Admin,Pastor,Deacon")]
        [HttpGet("role/{roleName}")]
        public async Task<IActionResult> GetUsersByRole(string roleName)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            // You can now use userId to filter or perform actions based on the authenticated user

            var userDtos = await _userService.GetUsersByRole(roleName);
            return Ok(userDtos);
        }

        [Authorize(Roles = "Admin,Pastor,Deacon")]
        [HttpGet("search/{name}")]
        public async Task<IActionResult> SearchUsersByName(string name)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            // You can now use userId to filter or perform actions based on the authenticated user

            var userDtos = await _userService.SearchUsersByName(name);
            return Ok(userDtos);
        }
    }
}
