using Church.Models;

namespace Church.RepositoryInterfaces
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetNotifications(string userId);
        Task<Notification> CreateNotification(Notification notification);
        Task MarkAsRead(string notificationId);
        Task DeactivateNotification(string notificationId);
    }

}
