using Dhoojol.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Dhoojol.Infrastructure.EfCore;

public class DhoojolContext : DbContext
{
    public DhoojolContext(DbContextOptions options) : base(options)
    {
    }

    protected DhoojolContext()
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }


}

