
using Dhoojol.Domain.Entities.Clients;
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

    //Allow CRUD operations
    public DbSet<User> Users { get; set; }
    public DbSet<Client> Clients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }


}

