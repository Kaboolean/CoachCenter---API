using Dhoojol.Application.Services.Auth;
using Dhoojol.Application.Services.Clients;
using Dhoojol.Application.Services.Coaches;
using Dhoojol.Application.Services.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dhoojol.Application.Extensions
{
    public static class ApplicationIocExtensions
    {
        public static IServiceCollection AddApplicationImplementations(this IServiceCollection services, IConfiguration configuration)
        {
            //sert à la dependency injection
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IClientsService, ClientsService>();
            services.AddScoped<ICoachesService, CoachesService>();
            
            return services;
        }
    }
}
