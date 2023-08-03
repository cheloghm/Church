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

            // Add other services and repositories as needed

            return services;
        }
    }
}
