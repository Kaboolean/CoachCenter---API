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
                new User { UserName = "ajoly", BirthDate = new DateTime(1993, 12, 19), Email = "joly.alexandre@hotmail.Fr", FirstName = "Alexandre", LastName = "Joly", Password = BCrypt.Net.BCrypt.HashPassword("123"), UserType = "coach" },
                new User { UserName = "ddhooghe", BirthDate = new DateTime(1993, 6, 8), Email = "ddhooghe@gmail.com", FirstName = "David", LastName = "D'hooghe", Password = BCrypt.Net.BCrypt.HashPassword("123"), UserType = "coach" },
            };

            await _dbContext.AddRangeAsync(users);

            await _dbContext.SaveChangesAsync();
        }
    }
}

