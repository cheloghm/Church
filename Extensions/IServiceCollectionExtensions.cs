using Church.RepositoryInterfaces;
using Church.ServiceInterfaces;
using Church.Repositories;
using Church.Services;
using Microsoft.Extensions.DependencyInjection;
using Church.Models;

namespace Church.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddMyServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IGenericService<Visitor>, GenericService<Visitor>>();
            services.AddScoped<IGenericRepository<Visitor>, GenericRepository<Visitor>>();
            services.AddScoped<IGenericService<Announcement>, GenericService<Announcement>>();
            services.AddScoped<IGenericRepository<Announcement>, GenericRepository<Announcement>>();
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<IRequestRepository, RequestRepository>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IProfilePhotoService, ProfilePhotoService>();
            services.AddScoped<IProfilePhotoRepository, ProfilePhotoRepository>();
            services.AddScoped<IBibleService, BibleService>();

            // Add other services and repositories as needed

            return services;
        }
    }
}
