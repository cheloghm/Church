using Church.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> AddProfilePhoto([FromBody] byte[] photo, string userId)
        {
            await _profilePhotoService.DeleteProfilePhotoByUserId(userId);

            var profilePhoto = await _profilePhotoService.AddProfilePhoto(photo, userId);
            return Ok(profilePhoto);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetProfilePhotoByUserId(string userId)
        {
            var profilePhoto = await _profilePhotoService.GetProfilePhotoByUserId(userId);

            if (profilePhoto == null)
                return NotFound();

            return Ok(profilePhoto);
        }
    }
}
