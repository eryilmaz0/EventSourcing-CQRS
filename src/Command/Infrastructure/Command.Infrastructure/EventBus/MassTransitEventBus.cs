using Command.Application.Abstracts.Infrastructure;
using EventSourcing.Shared.IntegrationEvent;
using MassTransit;

namespace Command.Infrastructure.EventBus;

public class MassTransitEventBus : IEventBus
{
    private readonly IPublishEndpoint _eventPublisher;

    public MassTransitEventBus(IPublishEndpoint eventPublisher)
    {
        _eventPublisher = eventPublisher;
    }

    public async Task PublishEventsAsync(IEnumerable<IIntegrationEvent> events)
    {
        foreach (var @event in events)
        {
            await _eventPublisher.Publish(@event, @event.GetType());
        }
    }
}