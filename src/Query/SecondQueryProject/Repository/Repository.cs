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
        _collection = context.GetCollection<T>(nameof(T));
    }
    

    public async Task<List<T>> GetAll()
    {
        return await _collection.Find(x => true).ToListAsync();
    }
    

    public async Task<T> Get(Expression<Func<T, bool>> predicate)
    {
        return await _collection.Find(predicate).FirstOrDefaultAsync();
    }
    

    public async Task<bool> Insert(T readModel)
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
    

    public async Task<bool> Update(T readModel)
    {
        try
        {
            var updateResult = await _collection.ReplaceOneAsync(filter: x => x.Id == readModel.Id, replacement: readModel);
            return updateResult.IsAcknowledged;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    

    public async Task<bool> Remove(T readModel)
    {
        try
        {
            var updateResult = await _collection.DeleteOneAsync(filter: x => x.Id == readModel.Id);
            return updateResult.IsAcknowledged;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}