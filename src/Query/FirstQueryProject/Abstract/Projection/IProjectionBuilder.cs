using EventSourcing.Shared.IntegrationEvent;

namespace FirstQueryProject.Abstract.Projection;

public interface IProjectionBuilder<T> where T : ReadModel.ReadModel
{
    public Task ProjectModelAsync(IIntegrationEvent @event);
}