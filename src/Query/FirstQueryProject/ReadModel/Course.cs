namespace FirstQueryProject.ReadModel;

public class Course : Abstract.ReadModel.ReadModel
{
    public Guid InstructorId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public DateTime Created { get; set; }
    public string Status { get; set; }
    public long Sections { get; set; }
    public long Participants { get; set; }
    public long Comments { get; set; }
}