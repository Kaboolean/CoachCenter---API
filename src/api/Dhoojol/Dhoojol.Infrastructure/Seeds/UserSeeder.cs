using Dhoojol.Domain.Entities.Clients;
using Dhoojol.Domain.Entities.Coaches;
using Dhoojol.Domain.Entities.Users;
using Dhoojol.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhoojol.Infrastructure.Seeds;

public class UserSeeder : ISeedDb
{
    private readonly DhoojolContext _dbContext;

    public UserSeeder(DhoojolContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAsync()
    {
        bool any = await _dbContext.Users.AnyAsync();

        if (!any)
        {
            var users = new List<User>
            {
                new User { UserName = "CoachOne", BirthDate = new DateTime(1990, 1, 1), Email = "CoachOne@test.com", FirstName = "CoachOne", LastName = "AlphaCoach", Password = BCrypt.Net.BCrypt.HashPassword("123"), UserType = "coach" },
                new User { UserName = "CoachTwo", BirthDate = new DateTime(1900, 1, 1), Email = "CoachTwo@test.com", FirstName = "CoachTwo", LastName = "BetaCoach", Password = BCrypt.Net.BCrypt.HashPassword("123"), UserType = "coach" },
                new User { UserName = "ClientOne", BirthDate = new DateTime(1900, 1, 1), Email = "ClientOne@test.com", FirstName = "ClientOne", LastName = "AlphaClient", Password = BCrypt.Net.BCrypt.HashPassword("123"), UserType = "client" },
            };
            var coaches = new List<Coach>
            {
                new Coach { User = users[0]},
                new Coach { User = users[1]},
            };
            var clients = new List<Client>
            {
                new Client { User = users[2]},
            };
            await _dbContext.AddRangeAsync(users);
            await _dbContext.AddRangeAsync(coaches);
            await _dbContext.AddRangeAsync(clients);

            await _dbContext.SaveChangesAsync();
        }
    }
}

