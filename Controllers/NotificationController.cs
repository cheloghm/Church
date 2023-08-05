using Church.DTO;
using Church.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Church.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetNotifications(string userId)
        {
            var notifications = await _notificationService.GetNotifications(userId);
            return Ok(notifications);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification(NotificationDTO notificationDto)
        {
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

    }

}
