using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Command.Application.Abstracts.Infrastructure;
using Command.Application.Abstracts.Persistence;
using Command.Application.Features.Course.Commands.CreateCourse;
using Command.Domain.DomainObject;
using Command.Domain.Enum;
using Command.Domain.Event;
using Command.Domain.Exception;
using EventSourcing.Shared.IntegrationEvent;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Command.UnitTests;

public class CreateCourseCommandHandlerUnitTests
{
    private Mock<IRepository<Course>> _mockRepository;
    private Mock<IEventBus> _mockEventBus;
    
    
    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IRepository<Course>>();
        _mockEventBus = new Mock<IEventBus>();
    }

    [Test]
    public async Task CreateCourseCommand_WithNotValidCourseStatus_ShouldThrowDomainException()
    {
        #region Arrange
        
        Course course = new(Guid.NewGuid())
        {
            CurrentState = new()
            {
                Status = CourseStatus.Created
            }
        };

        CreateCourseCommand command = new()
        {
            InstructorId = Guid.NewGuid(),
            Title = "New Course",
            Description = "New Course",
            Category = "Event Sourcing"
        };
        
        _mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(course);
        CreateCourseCommandHandler commandHandler = new(_mockRepository.Object, _mockEventBus.Object);
        

        #endregion

        #region Act

        var commandResult = async () => await commandHandler.Handle(command, new CancellationToken());

        #endregion

        #region Assertion
        
        await commandResult.Should().ThrowAsync<DomainException>(); 
        _mockRepository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _mockEventBus.Verify(x => x.PublishEventsAsync(It.IsAny<IEnumerable<IIntegrationEvent>>()), Times.Never);

        #endregion
    }
    
    
    
    [Test]
    public async Task CreateCourseCommand_WithNonCreatedCourseStatus_ShouldReturnSuccess()
    {
        #region Arrange
        
        Course course = new(Guid.Empty);
        
    
        CreateCourseCommand command = new()
        {
            InstructorId = Guid.NewGuid(),
            Title = "New Course",
            Description = "New Course",
            Category = "Event Sourcing"
        };
        
        _mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(course);
        CreateCourseCommandHandler commandHandler = new(_mockRepository.Object, _mockEventBus.Object);
        
    
        #endregion
        
        #region Act
    
        var result = await commandHandler.Handle(command, new CancellationToken());
    
        #endregion
        
        #region Assertion
    
        result.IsSuccess.Should().BeTrue();
        _mockRepository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _mockEventBus.Verify(x => x.PublishEventsAsync(It.IsAny<IEnumerable<IIntegrationEvent>>()), Times.Once);
    
        #endregion
    }
}