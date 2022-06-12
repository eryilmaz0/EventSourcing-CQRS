using Command.Domain.DomainObject;

namespace Command.Application.Abstracts.Persistence;

public interface IRepository<T> where T : AggregateRoot
{
    public Task<T> GetById(Guid aggregateId);
    public Task Save(T aggregate);
}