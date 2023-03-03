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
                new User { UserName = "User123", BirthDate = new DateTime(1990, 1, 1), Email = "user123@test.com", FirstName = "Jean", LastName = "Robert", Password = BCrypt.Net.BCrypt.HashPassword("123"), UserType = "coach" },
                new User { UserName = "Davdhoo", BirthDate = new DateTime(1900, 1, 1), Email = "davdhoo@test.com", FirstName = "David", LastName = "Dhoo", Password = BCrypt.Net.BCrypt.HashPassword("123"), UserType = "coach" },
            };

            await _dbContext.AddRangeAsync(users);

            await _dbContext.SaveChangesAsync();
        }
    }
}

