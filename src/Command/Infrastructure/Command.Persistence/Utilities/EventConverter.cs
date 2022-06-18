using System.Text.Json;
using Command.Domain.Enum;
using Command.Domain.Event;
using Command.Domain.Event.StoredEvent;

namespace Command.Persistence.Utilities;

public static class EventConverter
{
    public static IEnumerable<IDomainEvent> DeserializePersistentEvents(IEnumerable<PersistentEvent> events)
    {
        List<IDomainEvent> deserializedEvents = new();

        foreach (var @event in events)
        {
            deserializedEvents.Add(DeserializeEvent(@event));
        }

        return deserializedEvents;
    }


    private static IDomainEvent DeserializeEvent(PersistentEvent @event)
    {
        return @event.Type switch
        {
            EventType.CommentedCourse => JsonSerializer.Deserialize<CommentedCourseDomainEvent>(@event.Payload),
            EventType.CourseActivated => JsonSerializer.Deserialize<CourseActivatedDomainEvent>(@event.Payload),
            EventType.CourseCompleted => JsonSerializer.Deserialize<CourseCompletedDomainEvent>(@event.Payload),
            EventType.CourseCreatedEvent => JsonSerializer.Deserialize<CourseCreatedDomainEvent>(@event.Payload),
            EventType.CourseDescriptionChanged => JsonSerializer.Deserialize<CourseDescriptionChangedDomainEvent>(@event.Payload),
            EventType.CoursePrePresented => JsonSerializer.Deserialize<CoursePrePresentedDomainEvent>(@event.Payload),
            EventType.CourseTitleChanged => JsonSerializer.Deserialize<CourseTitleChangedDomainEvent>(@event.Payload),
            EventType.JoinedToCourse => JsonSerializer.Deserialize<JoinedToCourseDomainEvent>(@event.Payload),
            EventType.LeftFromCourse => JsonSerializer.Deserialize<LeftFromCourseDomainEvent>(@event.Payload),
            EventType.SectionAppended => JsonSerializer.Deserialize<SectionAppendedDomainEvent>(@event.Payload),
            _ => throw new InvalidOperationException("Invalid Event Type.")
        };
    }
}