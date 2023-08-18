using Church.DTO;
using Church.Models;
using Church.ServiceInterfaces;
using Church.RepositoryInterfaces;

namespace Church.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public NotificationService(INotificationRepository notificationRepository, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<Notification>> GetNotifications(string userId)
        {
            return await _notificationRepository.GetNotifications(userId);
        }

        public async Task<Notification> CreateNotification(NotificationDTO notificationDto)
        {
            var notification = new Notification
            {
                UserId = notificationDto.UserId,
                Message = notificationDto.Message,
                DateCreated = DateTime.UtcNow,
                IsRead = false
            };

            return await _notificationRepository.CreateNotification(notification);
        }

        public async Task MarkAsRead(string notificationId)
        {
            await _notificationRepository.MarkAsRead(notificationId);
        }

        public async Task DeactivateNotification(string notificationId, string userId)
        {
            var user = await _userRepository.GetUserById(userId);
            var userRole = await _roleRepository.GetRoleById(user.RoleId);
            if (userRole.Name == "Admin" || userRole.Name == "Pastor" || userRole.Name == "Deacon")
            {
                await _notificationRepository.DeactivateNotification(notificationId);
            }
            else
            {
                throw new UnauthorizedAccessException("You do not have permission to deactivate this notification.");
            }
        }

        public async Task<Notification> GetNotificationById(string notificationId)
        {
            var notification = await _notificationRepository.GetNotificationById(notificationId);
            if (notification != null && !notification.IsRead)
            {
                notification.IsRead = true;
                await _notificationRepository.UpdateNotification(notification);
            }
            return notification;
        }

        public async Task UpdateNotification(Notification notification)
        {
            await _notificationRepository.UpdateNotification(notification);
        }

    }

}
