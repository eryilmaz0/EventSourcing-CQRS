using Command.Domain.DomainObject;
using Command.Domain.Event;
using EventSourcing.Shared.IntegrationEvent;

namespace Command.Application.Abstracts.CommandHandler;

public abstract class CommandHandler
{
    public IEnumerable<IIntegrationEvent> PrepareIntegrationEvents<T>(T aggregate) where T : AggregateRoot
    {
        List<IIntegrationEvent> integrationEvents = new List<IIntegrationEvent>();
        
        foreach (var @event in aggregate.RaisedEvents())
        {
            integrationEvents.Add(@event.ToIntegrationEvent(aggregate.AggregateId)); 
        }
        
        return integrationEvents.OrderBy(x => x.Created);
    }
}