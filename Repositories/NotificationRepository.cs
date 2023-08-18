using Church.Data;
using Church.Models;
using Church.RepositoryInterfaces;
using MongoDB.Driver;

namespace Church.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly DataContext _context;

        public NotificationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notification>> GetNotifications(string userId)
        {
            return await _context.Notifications.Find(n => n.UserId == userId).ToListAsync();
        }

        public async Task<Notification> CreateNotification(Notification notification)
        {
            await _context.Notifications.InsertOneAsync(notification);
            return notification;
        }

        public async Task MarkAsRead(string notificationId)
        {
            var filter = Builders<Notification>.Filter.Eq(n => n.Id, notificationId);
            var update = Builders<Notification>.Update.Set(n => n.IsRead, true);
            await _context.Notifications.UpdateOneAsync(filter, update);
        }

        public async Task DeactivateNotification(string notificationId)
        {
            var filter = Builders<Notification>.Filter.Eq(n => n.Id, notificationId);
            var update = Builders<Notification>.Update.Set(n => n.IsActive, false);
            await _context.Notifications.UpdateOneAsync(filter, update);
        }

        public async Task<Notification> GetNotificationById(string notificationId)
        {
            return await _context.Notifications.Find(n => n.Id == notificationId).FirstOrDefaultAsync();
        }

        public async Task UpdateNotification(Notification notification)
        {
            var filter = Builders<Notification>.Filter.Eq(n => n.Id, notification.Id);
            await _context.Notifications.ReplaceOneAsync(filter, notification);
        }

    }

}
