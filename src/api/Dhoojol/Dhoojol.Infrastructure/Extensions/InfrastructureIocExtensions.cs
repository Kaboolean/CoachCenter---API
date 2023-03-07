using Dhoojol.Infrastructure.EfCore;
using Dhoojol.Infrastructure.EfCore.Repositories.Auth;
using Dhoojol.Infrastructure.EfCore.Repositories.Base;
using Dhoojol.Infrastructure.EfCore.Repositories.Clients;
using Dhoojol.Infrastructure.EfCore.Repositories.Coaches;
using Dhoojol.Infrastructure.EfCore.Repositories.Users;
using Dhoojol.Infrastructure.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Dhoojol.Infrastructure.Extensions;

public static class InfrastructureIocExtensions
{
    public static IServiceCollection AddInfrastructureImplementations(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEfCore(configuration);

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<ICoachRepository, CoachRepository>();
        services.AddScoped<UserSeeder>();

        return services;
    }

    private static void AddEfCore(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Dhoojol");

        services.AddDbContext<DhoojolContext>(options => options.UseSqlServer(connectionString));
    }

    public async static Task MigrateDbAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<DhoojolContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<DhoojolContext>>();

        var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();

        if (pendingMigrations?.Any() == true)
        {
            logger.LogInformation("Migrate database...");
            await dbContext.Database.MigrateAsync();
            logger.LogInformation("Migrate migrated.");
        }

        var userSeeder = scope.ServiceProvider.GetRequiredService<UserSeeder>();
        await userSeeder.SeedAsync();
    }

}

