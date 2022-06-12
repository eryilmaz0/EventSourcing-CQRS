using Command.Domain.Event.StoredEvent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Command.Persistence.EntityConfiguration;

public class PersistentEventConfiguration : IEntityTypeConfiguration<PersistentEvent>
{
    public void Configure(EntityTypeBuilder<PersistentEvent> builder)
    {
        builder.ToTable("Events");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.AggregateId).IsRequired();
        builder.Property(x => x.Version).IsRequired();
        builder.Property(x => x.Type).IsRequired();
        builder.Property(x => x.Created).IsRequired();
        builder.Property(x => x.Payload).IsRequired();
    }
}