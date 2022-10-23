using Dhoojol.Domain.Entities.Users;
using Dhoojol.Domain.Entities.Products;
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
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }


}

