using MongoDB.Driver;
using SecondQueryProject.ReadModel;

namespace SecondQueryProject.Repository;

public class MongoContext
{

    private MongoClient _client;
    private IMongoDatabase _database;
    
    
    public MongoContext(IConfiguration config)
    {
        var client = new MongoClient(config.GetValue<string>("MongoDbConfig:ConnectionString"));
        var database = client.GetDatabase(config.GetValue<string>("MongoDbConfig:Database"));
    }
    
    
    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }

    
}