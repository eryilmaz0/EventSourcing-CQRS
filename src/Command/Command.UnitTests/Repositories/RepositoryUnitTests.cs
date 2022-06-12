using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Command.Application.Abstracts.Infrastructure;
using Command.Application.Abstracts.Persistence;
using Command.Domain.DomainObject;
using Command.Domain.Enum;
using Command.Domain.Event;
using Command.Persistence.Repository;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Command.UnitTests.Repositories;

public class RepositoryUnitTests
{
    private Mock<IEventStore> _mockEventStore;
    
    
    [SetUp]
    public void Setup()
    {
        _mockEventStore = new Mock<IEventStore>();
    }


    [Test]
    public async Task GetAggregateById_ShouldReturnEmptyAggregate()
    {
        Guid aggregateId = Guid.NewGuid();
        List<IEvent> events = new List<IEvent>();

        //Returns empty event list
        _mockEventStore.Setup(x => x.GetEventsAsync(aggregateId)).ReturnsAsync(events);
        IRepository<Course> repository = new Repository<Course>(_mockEventStore.Object);

        var aggregate = await repository.GetByIdAsync(aggregateId);

        aggregate.Version.Should().Be(0);
        aggregate.AggregateId.Should().Be(aggregateId);
        aggregate.CurrentState.Should().NotBeNull();
        aggregate.CurrentState.Status.Should().Be(CourseStatus.NonCreated);
    }
    
    
    [Test]
    public async Task GetAggregateById_ShouldReturnAggregateWithExpectedCurrentState()
    {
        Guid aggregateId = Guid.NewGuid();
        List<IEvent> events = new List<IEvent>()
        {
            new CourseCreatedEvent()
            {
                Created = DateTime.UtcNow,
                Title = "Event Sourcing Course",
                Description = "This is a new Course",
                Category = "Software",
                InstructorId = Guid.NewGuid()
            },

            new CourseTitleChangedEvent()
            {
                Created = DateTime.UtcNow,
                Title = "Event Sourcing New Course!"
            },
            
            new CourseDescriptionChangedEvent()
            {
                Created = DateTime.UtcNow,
                Description = "Event Sourcing Full Course"
            },
            
            new CoursePrePresentedEvent()
            {
                Created = DateTime.UtcNow
            }
        };

        //Returns empty event list
        _mockEventStore.Setup(x => x.GetEventsAsync(aggregateId)).ReturnsAsync(events);
        IRepository<Course> repository = new Repository<Course>(_mockEventStore.Object);

        var aggregate = await repository.GetByIdAsync(aggregateId);

        aggregate.Version.Should().Be(4);
        aggregate.AggregateId.Should().Be(aggregateId);
        aggregate.CurrentState.Should().NotBeNull();
        aggregate.CurrentState.Status.Should().Be(CourseStatus.PrePresentation);
    }
}