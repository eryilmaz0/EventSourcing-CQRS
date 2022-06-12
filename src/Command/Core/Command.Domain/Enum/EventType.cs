namespace Command.Domain.Enum;

//For PersistentEvent
public enum EventType  
{
    CourseCreatedEvent = 1,
    CourseTitleChanged,
    CourseDescriptionChanged,
    CoursePrePresented,
    CourseActivated,
    CourseDisabled,
    SectionAppended,
    JoinedToCourse,
    LeftFromCourse,
    CommentedCourse
}