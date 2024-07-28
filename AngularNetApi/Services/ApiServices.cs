using AngularNetApi.Services.DbServices;

namespace AngularNetApi.Services
{
    public static class ApiServices
    {
        public static IServiceCollection AllApiService(this IServiceCollection services)
        {
            services.AddScoped<DbService>();
            services.AddScoped<AuthService>();

            services.AddScoped<UserService>();

            services.AddScoped<DbContext>();

            return services;
        }
    }
}
