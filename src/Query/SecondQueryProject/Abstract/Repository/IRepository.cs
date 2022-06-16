using System.Linq.Expressions;
using SecondQueryProject.ReadModel;

namespace SecondQueryProject.Abstract.Repository;

public interface IRepository<T> where T : ReadModel.ReadModel
{
    public Task<List<T>> GetAll();
    public Task<T> Get(Expression<Func<T, bool>> predicate);
    public Task<bool> Insert(T readModel);
    public Task<bool> Update(T readModel);
    public Task<bool> Remove(T readModel);
}