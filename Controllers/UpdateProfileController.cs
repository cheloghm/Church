using Church.DTO;
using Church.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Church.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UpdateProfileController : ControllerBase
    {
        private readonly IUserService _userService;

        public UpdateProfileController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserProfile(UpdateUserDTO userDto)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var updatedUser = await _userService.UpdateUserProfile(userId, userDto);

            if (updatedUser == null)
                return NotFound();

            return Ok(updatedUser);
        }

    }
}
