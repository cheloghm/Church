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

        [Authorize(Roles = "Admin,Pastor,Deacon,Member")]
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

            if (string.IsNullOrEmpty(userId))
            {
                var userDto = await _userService.GetUserDetails(id);

                if (userDto == null)
                    return NotFound();

                return Ok(userDto);
            }
            return null;
            
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyDetails()
        {
            // Get the user's ID from the claims
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            // Check if the user ID is available
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            // Get the user details using the user ID
            var userDto = await _userService.GetUserDetails(userId);

            // Check if the user details are available
            if (userDto == null)
            {
                return NotFound();
            }

            return Ok(userDto);
        }

        [Authorize(Roles = "Admin,Pastor,Deacon")]
        [HttpGet("role/{roleName}")]
        public async Task<IActionResult> GetUsersByRole(string roleName)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            // You can now use userId to filter or perform actions based on the authenticated user
            if (userId == null)
            {
                return NotFound();
            }
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
