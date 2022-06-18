using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SecondQueryProject.Abstract.ReadModel;

public abstract class ReadModel
{
    public Guid AggregateId { get; set; }
    public DateTime Created { get; set; }
}