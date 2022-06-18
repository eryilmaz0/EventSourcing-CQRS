using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Command.Application.Abstracts.Infrastructure;
using Command.Application.Abstracts.Persistence;
using Command.Application.Features.Course.Commands.CommentCourse;
using Command.Application.Features.Course.Commands.LeaveCourse;
using Command.Domain.DomainObject;
using Command.Domain.Enum;
using Command.Domain.Exception;
using EventSourcing.Shared.IntegrationEvent;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Command.UnitTests;

public class CommentCourseCommandHandlerUnitTests
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
    public async Task CommentCourseCommand_WithInvalidStatus_ShouldThrowDomainException()
    {
        #region Arrange

        Guid aggregateId = Guid.NewGuid();
        Guid participantId = Guid.NewGuid();

        Course course = new()
        {
            CurrentState = new()
            {
                //We cant comment to pre presenting courses. Should throw exception
                Status = CourseStatus.PrePresentation,
            }
        };

        CommentCourseCommand command = new()
        {
            CourseId = aggregateId,
            CommentorId = participantId,
            Comment = "That was Awesome!"
        };

        _mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(course);
        CommentCourseCommandHandler commandHandler = new(_mockRepository.Object, _mockEventBus.Object);

        #endregion

        #region Act

        var commandAct = async () => await commandHandler.Handle(command, new CancellationToken());

        #endregion
        
        #region Assertion

        await commandAct.Should().ThrowAsync<DomainException>();
        _mockRepository.Verify(x => x.GetByIdAsync(aggregateId), Times.Once);
        _mockEventBus.Verify(x => x.PublishEventsAsync(It.IsAny<IEnumerable<IIntegrationEvent>>()), Times.Never);

        #endregion
    }
    
    
    [Test]
    public async Task CommentCourseCommand_WithNonParticipantId_ShouldThrowDomainException()
    {
        #region Arrange

        Guid aggregateId = Guid.NewGuid();
        Guid participantId = Guid.NewGuid();

        Course course = new()
        {
            CurrentState = new()
            {
                Status = CourseStatus.Activated,
                Participants = { new(){ParticipantId = Guid.NewGuid(), JoinDate = DateTime.UtcNow} }
                
            }
        };

        CommentCourseCommand command = new()
        {
            CourseId = aggregateId,
            CommentorId = participantId,
            Comment = "That was Awesome!"
        };

        _mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(course);
        CommentCourseCommandHandler commandHandler = new(_mockRepository.Object, _mockEventBus.Object);

        #endregion

        #region Act

        var commandAct = async () => await commandHandler.Handle(command, new CancellationToken());

        #endregion
        
        #region Assertion

        await commandAct.Should().ThrowAsync<DomainException>();
        _mockRepository.Verify(x => x.GetByIdAsync(aggregateId), Times.Once);
        _mockEventBus.Verify(x => x.PublishEventsAsync(It.IsAny<IEnumerable<IIntegrationEvent>>()), Times.Never);

        #endregion
    }
    
    
    [Test]
    public async Task CommentCourseCommand_WithValidParameters_ShouldReturnSuccess()
    {
        #region Arrange

        Guid aggregateId = Guid.NewGuid();
        Guid participantId = Guid.NewGuid();

        Course course = new()
        {
            CurrentState = new()
            {
                Status = CourseStatus.Activated,
                Participants = { new(){ParticipantId = participantId, JoinDate = DateTime.UtcNow} }
                
            }
        };

        CommentCourseCommand command = new()
        {
            CourseId = aggregateId,
            CommentorId = participantId,
            Comment = "That was Awesome!"
        };

        _mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(course);
        CommentCourseCommandHandler commandHandler = new(_mockRepository.Object, _mockEventBus.Object);

        #endregion

        #region Act

        var commandResult =  await commandHandler.Handle(command, new CancellationToken());

        #endregion
        
        #region Assertion

        commandResult.IsSuccess.Should().BeTrue();
        _mockRepository.Verify(x => x.GetByIdAsync(aggregateId), Times.Once);
        _mockEventBus.Verify(x => x.PublishEventsAsync(It.IsAny<IEnumerable<IIntegrationEvent>>()), Times.Once);

        #endregion
    }
}