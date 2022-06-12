using Command.Application.Abstracts.Persistence;
using Command.Persistence.Context;
using Command.Persistence.EventStore;
using Command.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Command.Persistence;

public static class ServiceRegistrator
{
    public static IServiceCollection RegisterPersistenceServices(this IServiceCollection serviceCollection, IConfiguration config)
    {
        serviceCollection.AddDbContext<EventSourcingDbContext>(opts =>
                         opts.UseNpgsql(config.GetConnectionString("EventStoreDb")));

        serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        serviceCollection.AddScoped<IEventStore, EfEventStore>();

        return serviceCollection;
    }
}