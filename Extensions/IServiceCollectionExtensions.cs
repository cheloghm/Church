using Church.RepositoryInterfaces;
using Church.ServiceInterfaces;
using Church.Repositories;
using Church.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Church.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddMyServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IVisitorService, VisitorService>();
            services.AddScoped<IVisitorRepository, VisitorRepository>();
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
