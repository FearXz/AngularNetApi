using AngularNetApi.Application.Mapping;
using AngularNetApi.Application.Services;
using AngularNetApi.Infrastructure.Interfaces;
using AngularNetApi.Infrastructure.Repositories;
using AngularNetApi.Infrastructure.Services.Email;
using AngularNetApi.Services.Auth;
using AngularNetApi.Services.User;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace AngularNetApi.Infrastructure.Services
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
                cfg.AddProfile<MappingProfile>();
            });

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddSingleton<EmailTemplate>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();

            return services;
        }
    }
}
