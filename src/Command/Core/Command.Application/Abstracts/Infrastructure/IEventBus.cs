using EventSourcing.Shared.IntegrationEvent;

namespace Command.Application.Abstracts.Infrastructure;

public interface IEventBus
{
    public Task PublishEventsAsync(IEnumerable<IIntegrationEvent> events);
}