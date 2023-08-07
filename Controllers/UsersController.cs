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
        private readonly IUserService _userService; // Assuming you have a service to get profile photos

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var userDtos = await _userService.GetAllUsers();
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDetails(string id)
        {
            var userDto = await _userService.GetUserDetails(id);

            if (userDto == null)
                return NotFound();

            return Ok(userDto);
        }

        [HttpGet("role/{roleName}")]
        public async Task<IActionResult> GetUsersByRole(string roleName)
        {
            var userDtos = await _userService.GetUsersByRole(roleName);
            return Ok(userDtos);
        }

        [HttpGet("search/{name}")]
        public async Task<IActionResult> SearchUsersByName(string name)
        {
            var userDtos = await _userService.SearchUsersByName(name);
            return Ok(userDtos);
        }

    }
}
