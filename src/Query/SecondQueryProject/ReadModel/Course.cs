using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SecondQueryProject.ReadModel;

public class Course : Abstract.ReadModel.ReadModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public Guid InstructorId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Status { get; set; }

    public ICollection<Section> Sections { get; set; }
    public ICollection<Participant> Participants { get; set; }
    public ICollection<Comment> Comments { get; set; }


    public Course()
    {
        this.Created = DateTime.UtcNow;
        this.Sections = new List<Section>();
        this.Participants = new List<Participant>();
        this.Comments = new List<Comment>();
    }
}