using Dhoojol.Domain.Entities.Clients;
using Dhoojol.Domain.Entities.Coaches;
using Dhoojol.Domain.Entities.Users;
using Dhoojol.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

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

            var clients = new List<Client>();
            var coaches = new List<Coach>();

            foreach(User user in users)
            {
                if(user.UserType == "client")
                {
                    clients.Add(new Client { User = user});
                }
                if (user.UserType == "coach")
                {
                    coaches.Add(new Coach { User = user });
                }
            }

            await _dbContext.AddRangeAsync(users);
            await _dbContext.AddRangeAsync(coaches);
            await _dbContext.AddRangeAsync(clients);

            await _dbContext.SaveChangesAsync();
        }
    }
}

