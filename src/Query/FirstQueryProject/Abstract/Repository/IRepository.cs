using System.Linq.Expressions;

namespace FirstQueryProject.Abstract.Repository;

public interface IRepository<T> where T : ReadModel.ReadModel
{
    public Task<List<T>> GetAllAsync();
    public Task<T> GetAsync(Func<T, bool> predicate);
    public Task<bool> InsertAsync(T readModel);
    public Task<bool> UpdateAsync(T readModel);
    public Task<bool> RemoveAsync(T readModel);
}