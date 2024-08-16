using AngularNetApi.Interfaces;
using AngularNetApi.MappingProfile;
using AngularNetApi.Repository;
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
            services.AddAutoMapper(typeof(AuthMappingProfile));

            services.AddScoped<EUserManager>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
