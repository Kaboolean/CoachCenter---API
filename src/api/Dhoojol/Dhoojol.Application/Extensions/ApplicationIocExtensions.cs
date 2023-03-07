using Dhoojol.Application.Services.Auth;
using Dhoojol.Application.Services.Clients;
using Dhoojol.Application.Services.Coaches;
using Dhoojol.Application.Services.Users;
using Dhoojol.Infrastructure.EfCore.Repositories.Users;
using Dhoojol.Infrastructure.Seeds;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
