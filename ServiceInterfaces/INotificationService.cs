using Church.DTO;
using Church.Models;

namespace Church.ServiceInterfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> GetNotifications(string userId);
        Task<Notification> CreateNotification(NotificationDTO notificationDto);
        Task MarkAsRead(string notificationId);
        Task DeactivateNotification(string notificationId, string userId);
    }

}
