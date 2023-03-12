using Dhoojol.Domain.Entities.Clients;
using Dhoojol.Domain.Entities.Coaches;
using Dhoojol.Domain.Entities.Sessions;
using Dhoojol.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

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
    public DbSet<Coach> Coaches { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<SessionParticipant> SessionsParticipants{ get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //clé composé (composite key)
        //modelBuilder.Entity<SessionParticipant>().HasKey(e => new {e.SessionId, e.ClientId });
        modelBuilder.Entity<SessionParticipant>().HasOne(e => e.Client).WithMany(e=>e.Participations).HasForeignKey(e=>e.ClientId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<SessionParticipant>().HasOne(e => e.Session).WithMany(e => e.Participants).HasForeignKey(e=>e.SessionId).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Session>().Property(e => e.Tags).HasConversion(new ValueConverter<List<string>, string>(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
            v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null)));

    }
}

