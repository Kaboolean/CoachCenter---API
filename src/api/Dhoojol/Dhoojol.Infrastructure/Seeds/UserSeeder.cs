using Dhoojol.Domain.Entities.Clients;
using Dhoojol.Domain.Entities.Coaches;
using Dhoojol.Domain.Entities.Sessions;
using Dhoojol.Domain.Entities.Users;
using Dhoojol.Domain.Enums;
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
        var users = new List<User>
            {
                new User { UserName = "CoachOne", BirthDate = new DateTime(1990, 1, 1), Email = "CoachOne@test.com", FirstName = "Eric", LastName = "Dupont", Password = BCrypt.Net.BCrypt.HashPassword("111111"), UserType = UserType.Coach },
                new User { UserName = "ClientOne", BirthDate = new DateTime(1900, 1, 1), Email = "ClientOne@test.com", FirstName = "Robert", LastName = "Durand", Password = BCrypt.Net.BCrypt.HashPassword("111111"), UserType = UserType.Client },
                new User { UserName = "ClientTwo", BirthDate = new DateTime(1900, 1, 1), Email = "ClientTwo@test.com", FirstName = "Roger", LastName = "Richard", Password = BCrypt.Net.BCrypt.HashPassword("111111"), UserType = UserType.Client },
                new User { UserName = "ClientThree", BirthDate = new DateTime(1900, 1, 1), Email = "ClientThree@test.com", FirstName = "Paul", LastName = "Martin", Password = BCrypt.Net.BCrypt.HashPassword("111111"), UserType = UserType.Client },
            };
            var clients = new List<Client>();
            var coaches = new List<Coach>();
        if (!any)
        {
            

            
            int num = 1;
            foreach(User user in users)
            {
                
                if(user.UserType == UserType.Client)
                {
                    clients.Add(new Client { Goal="Be strong", Age=18+num, Height=175+num,Weight=75+num, Handicap="None" ,User = user});
                }
                if (user.UserType == UserType.Coach)
                {
                    coaches.Add(new Coach { User = user, Grades="bac+"+num, Description="I am number "+num, HourlyRate= 50 +num});
                }
                num++;
            }
            var sessions = new Session
            {
                Name = "Renfo musculaire", Date = DateTime.Now, Location = "Lille, 2 Avenue du Maine", Duration=60, Description="Petite session de renfo haut de corps", Coach = coaches[0], Tags= new List<string>
                {
                    "Musculation", "Débutant", "Basic-fit", "Intérieur", "Dos", "Bras"
                }
            };
            var sessionParticipant = new List<SessionParticipant>
            {
                new SessionParticipant{SessionId = sessions.Id, ClientId = clients[0].Id },
            new SessionParticipant { SessionId = sessions.Id, ClientId = clients[1].Id },
                new SessionParticipant{SessionId = sessions.Id, ClientId = clients[2].Id }
            };
            await _dbContext.AddRangeAsync(users);
            await _dbContext.AddRangeAsync(coaches);
            await _dbContext.AddRangeAsync(clients);
            await _dbContext.AddRangeAsync(sessions);
            await _dbContext.AddRangeAsync(sessionParticipant);

            await _dbContext.SaveChangesAsync();
        }

            

    }
}

