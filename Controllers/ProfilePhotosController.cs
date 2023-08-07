using Church.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Church.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilePhotosController : ControllerBase
    {
        private readonly IProfilePhotoService _profilePhotoService;

        public ProfilePhotosController(IProfilePhotoService profilePhotoService)
        {
            _profilePhotoService = profilePhotoService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProfilePhoto([FromBody] byte[] photo)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            await _profilePhotoService.DeleteProfilePhotoByUserId(userId);

            var profilePhoto = await _profilePhotoService.AddProfilePhoto(photo, userId);
            return Ok(profilePhoto);
        }

        [HttpGet]
        public async Task<IActionResult> GetProfilePhotoByUserId()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var profilePhoto = await _profilePhotoService.GetProfilePhotoByUserId(userId);

            if (profilePhoto == null)
                return NotFound();

            return Ok(profilePhoto);
        }
    }
}
