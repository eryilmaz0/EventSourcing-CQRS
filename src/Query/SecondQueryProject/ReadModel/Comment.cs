namespace SecondQueryProject.ReadModel;

public class Comment 
{
    public Guid CommentorId { get; set; }
    public string Content { get; set; }
    public DateTime Created { get; set; }
}