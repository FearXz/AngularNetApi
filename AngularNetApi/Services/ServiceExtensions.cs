using AngularNetApi.Interfaces;
using AngularNetApiAngularNetApi.Services;

namespace AngularNetApi.Services
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterAllServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddScoped<EUserManager>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
