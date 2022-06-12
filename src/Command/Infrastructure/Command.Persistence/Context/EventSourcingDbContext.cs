using Command.Domain.Event.StoredEvent;
using Command.Persistence.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Command.Persistence.Context;

public class EventSourcingDbContext : DbContext
{
    public EventSourcingDbContext(DbContextOptions<EventSourcingDbContext> options):base(options)
    {
        
    }


    public DbSet<PersistentEvent> Events { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PersistentEventConfiguration());
    }
}