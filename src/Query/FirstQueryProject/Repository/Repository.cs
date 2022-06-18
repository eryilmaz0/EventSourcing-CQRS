using System.Linq.Expressions;
using FirstQueryProject.Abstract.Repository;
using FirstQueryProject.ReadModel;
using Newtonsoft.Json;
using StackExchange.Redis;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace FirstQueryProject.Repository;

public class Repository<T> : IRepository<T> where T : Abstract.ReadModel.ReadModel
{
    private IConnectionMultiplexer _connection;
    private IDatabase _database;
    
    public Repository(IConfiguration config)
    {
        //Will be Singleton
        string host = config.GetValue<string>("RedisConfig:Host");
        int dbIndex = config.GetValue<int>("RedisConfig:DbIndex");
        string password = config.GetValue<string>("RedisConfig:Password");
        Connect(host, dbIndex, password);
    }


    private async Task Connect(string host, int dbIndex, string password)
    {
        var redisOptions = new ConfigurationOptions()
        {
            EndPoints = { host },
            DefaultDatabase = dbIndex,
            Password = password,
            SyncTimeout = 10000
        };
        
        this._connection = await ConnectionMultiplexer.ConnectAsync(redisOptions);
        this._database = _connection.GetDatabase();
    }
    
    
    public async Task<List<T>> GetAllAsync()
    {
        if (await _database.KeyExistsAsync("courses"))
        {
            var value = await _database.StringGetAsync("courses");
            return JsonSerializer.Deserialize<List<T>>(value);
        }

        return new List<T>();
    }

    
    public async Task<T> GetAsync(Func<T, bool> predicate)
    {
        if (await _database.KeyExistsAsync("courses"))
        {
            var value = await _database.StringGetAsync("courses");
            return JsonSerializer.Deserialize<List<T>>(value).FirstOrDefault(predicate);
        }
        
        return null;
    }
    

    public async Task<bool> InsertAsync(T readModel)
    {
        if (await _database.KeyExistsAsync("courses"))
        {
            var value = await _database.StringGetAsync("courses");
            var courses = JsonSerializer.Deserialize<List<T>>(value).ToList();
            courses.Add(readModel);
            await _database.StringSetAsync("courses", JsonSerializer.Serialize(courses));
            return true;
        }


        List<T> newList = new() { readModel };
        await _database.StringSetAsync("courses", JsonSerializer.Serialize(newList));
        return true;
    }

    
    public async Task<bool> UpdateAsync(T readModel)
    {
        var value = await _database.StringGetAsync("courses");
        var courses = JsonSerializer.Deserialize<List<T>>(value).ToList();
        var course = courses.FirstOrDefault(x => x.Id == readModel.Id);

        if (course is null)
            return false;

        courses.Remove(course);
        courses.Add(readModel);
        await _database.StringSetAsync("courses", JsonSerializer.Serialize(courses));
        return true;
    }
    

    public async Task<bool> RemoveAsync(T readModel)
    {
        var value = await _database.StringGetAsync("courses");
        var courses = JsonSerializer.Deserialize<List<T>>(value).ToList();
        var course = courses.FirstOrDefault(x => x.Id == readModel.Id);

        if (course is null)
            return false;
        
        courses.Remove(course);
        await _database.StringSetAsync("courses", JsonSerializer.Serialize(courses));
        return true;
    }
}