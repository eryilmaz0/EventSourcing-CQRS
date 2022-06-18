using System.Linq.Expressions;
using MongoDB.Driver;
using SecondQueryProject.Abstract;
using SecondQueryProject.Abstract.Repository;
using SecondQueryProject.ReadModel;

namespace SecondQueryProject.Repository;

public class Repository<T> : IRepository<T> where T : Abstract.ReadModel.ReadModel
{
    private readonly IMongoCollection<T> _collection;

    public Repository(MongoContext context)
    {
        _collection = context.GetCollection<T>(typeof(T).Name);
    }
    

    public async Task<List<T>> GetAllAsync()
    {
        return await _collection.Find(x => true).ToListAsync();
    }
    

    public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await _collection.Find(predicate).FirstOrDefaultAsync();
    }
    

    public async Task<bool> InsertAsync(T readModel)
    {
        try
        {
            await _collection.InsertOneAsync(readModel);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    

    public async Task<bool> UpdateAsync(T readModel)
    {
        try
        {
            var updateResult = await _collection.ReplaceOneAsync(filter: x => x.AggregateId == readModel.AggregateId, replacement: readModel);
            return updateResult.IsAcknowledged;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    

    public async Task<bool> RemoveAsync(T readModel)
    {
        try
        {
            var updateResult = await _collection.DeleteOneAsync(filter: x => x.AggregateId == readModel.AggregateId);
            return updateResult.IsAcknowledged;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}