using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SecondQueryProject.Abstract.ReadModel;

public abstract class ReadModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public DateTime Created { get; set; }
}