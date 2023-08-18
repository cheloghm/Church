using Church.DTO;
using Church.Models;
using Church.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Church.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var notifications = await _notificationService.GetNotifications(userId);
            return Ok(notifications);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification(NotificationDTO notificationDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            notificationDto.UserId = userId; // Assuming NotificationDTO has a UserId property

            var notification = await _notificationService.CreateNotification(notificationDto);
            return Ok(notification);
        }

        [HttpPut("{notificationId}")]
        public async Task<IActionResult> MarkAsRead(string notificationId)
        {
            await _notificationService.MarkAsRead(notificationId);
            return NoContent();
        }

        [Authorize(Roles = "Admin,Pastor,Deacon")]
        [HttpPut("deactivate/{id}")]
        public async Task<IActionResult> DeactivateNotification(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get userId from the authenticated user's claims
            await _notificationService.DeactivateNotification(id, userId);
            return Ok();
        }

        [HttpGet("{notificationId}")]
        public async Task<IActionResult> GetNotificationById(string notificationId)
        {
            var notification = await _notificationService.GetNotificationById(notificationId);
            if (notification == null)
                return NotFound();
            return Ok(notification);
        }

        [Authorize(Roles = "Admin,Pastor,Deacon")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateNotification(Notification notification)
        {
            await _notificationService.UpdateNotification(notification);
            return Ok();
        }

        [HttpGet("undeactivated")]
        public async Task<IActionResult> GetUndeactivatedNotifications()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var notifications = await _notificationService.GetUndeactivatedNotifications(userId);
            return Ok(notifications);
        }

    }

}
