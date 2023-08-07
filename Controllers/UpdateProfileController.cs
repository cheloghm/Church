using Church.DTO;
using Church.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserProfile(string id, UserDTO userDto)
        {
            var updatedUser = await _userService.UpdateUserProfile(id, userDto);

            if (updatedUser == null)
                return NotFound();

            return Ok(updatedUser);
        }
    }
}
