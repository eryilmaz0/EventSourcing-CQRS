using System.Security.Cryptography.X509Certificates;
using Command.Domain.DomainObject;

namespace Command.Application.Abstracts.Persistence;

public interface IRepository<T> where T : AggregateRoot
{
    public Task<T> GetByIdAsync(Guid aggregateId);
    public Task SaveAsync(T aggregate);
}