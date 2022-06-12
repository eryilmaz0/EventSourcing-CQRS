using System.Text.Json;
using Command.Domain.Enum;
using Command.Domain.Event;
using Command.Domain.Event.StoredEvent;

namespace Command.Persistence.Utilities;

public static class EventConverter
{
    public static IEnumerable<IEvent> DeserializePersistentEvents(IEnumerable<PersistentEvent> events)
    {
        List<IEvent> deserializedEvents = new();

        foreach (var @event in events)
        {
            deserializedEvents.Add(DeserializeEvent(@event));
        }

        return deserializedEvents;
    }


    private static IEvent DeserializeEvent(PersistentEvent @event)
    {
        return @event.Type switch
        {
            EventType.CommentedCourse => JsonSerializer.Deserialize<CommentedCourseEvent>(@event.Payload),
            EventType.CourseActivated => JsonSerializer.Deserialize<CourseActivatedEvent>(@event.Payload),
            EventType.CourseCompleted => JsonSerializer.Deserialize<CourseCompletedEvent>(@event.Payload),
            EventType.CourseCreatedEvent => JsonSerializer.Deserialize<CourseCreatedEvent>(@event.Payload),
            EventType.CourseDescriptionChanged => JsonSerializer.Deserialize<CourseDescriptionChangedEvent>(@event.Payload),
            EventType.CoursePrePresented => JsonSerializer.Deserialize<CoursePrePresentedEvent>(@event.Payload),
            EventType.CourseTitleChanged => JsonSerializer.Deserialize<CourseTitleChangedEvent>(@event.Payload),
            EventType.JoinedToCourse => JsonSerializer.Deserialize<JoinedToCourseEvent>(@event.Payload),
            EventType.LeftFromCourse => JsonSerializer.Deserialize<LeftFromCourseEvent>(@event.Payload),
            EventType.SectionAppended => JsonSerializer.Deserialize<SectionAppendedEvent>(@event.Payload),
            _ => throw new InvalidOperationException("Invalid Event Type.")
        };
    }
}