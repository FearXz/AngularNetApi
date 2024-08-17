using AngularNetApi.MappingProfile;
using AngularNetApi.Repository.User;
using AngularNetApi.Services.Auth;
using AngularNetApi.Services.User;

namespace AngularNetApi.Services
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterAllServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AuthMappingProfile>();
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
