using Church.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Church.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDetails(string id)
        {
            var user = await _userService.GetUserDetails(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
