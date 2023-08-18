using Church.Models;

namespace Church.RepositoryInterfaces
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetNotifications(string userId);
        Task<Notification> CreateNotification(Notification notification);
        Task MarkAsRead(string notificationId);
        Task DeactivateNotification(string notificationId);
        Task<Notification> GetNotificationById(string notificationId);
        Task UpdateNotification(Notification notification);
        Task<IEnumerable<Notification>> GetUndeactivatedNotifications(string userId);

    }

}
