using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Command.Application.Abstracts.Infrastructure;
using Command.Application.Abstracts.Persistence;
using Command.Application.Features.Course.Commands.ChangeCourseDescription;
using Command.Application.Features.Course.Commands.ChangeCourseTitle;
using Command.Domain.DomainObject;
using Command.Domain.Enum;
using Command.Domain.Exception;
using EventSourcing.Shared.IntegrationEvent;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Command.UnitTests;

public class ChangeCourseDescriptionCommandHandlerUnitTests
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
    public async Task ChangeCourseDescriptionCommand_WithNotValidStatus_ShouldThrowDomainException()
    {
        #region Arrange

        Guid aggregateId = Guid.NewGuid();
        Course course = new()
        {
            CurrentState = new()
            {
                Status = CourseStatus.NonCreated
            }
        };

        ChangeCourseDescriptionCommand command = new()
        {
            CourseId = aggregateId,
            Description = "Changed Description"
        };

        _mockRepository.Setup(x => x.GetByIdAsync(aggregateId)).ReturnsAsync(course);
        ChangeCourseDescriptionCommandHandler commandHandler = new(_mockRepository.Object, _mockEventBus.Object);

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
    public async Task ChangeCourseDescriptionCommand_WithCreatedStatus_ShouldReturnSuccess()
    {
        #region Arrange

        Guid aggregateId = Guid.NewGuid();
        Course course = new()
        {
            CurrentState = new()
            {
                Status = CourseStatus.Created
            }
        };

        ChangeCourseDescriptionCommand command = new()
        {
            CourseId = aggregateId,
            Description = "Changed Description"
        };

        _mockRepository.Setup(x => x.GetByIdAsync(aggregateId)).ReturnsAsync(course);
        ChangeCourseDescriptionCommandHandler commandHandler = new(_mockRepository.Object, _mockEventBus.Object);

        #endregion

        #region Act

        var commandResult = await commandHandler.Handle(command, new CancellationToken());

        #endregion
        
        #region Assertion

        commandResult.IsSuccess.Should().BeTrue();
        _mockRepository.Verify(x => x.GetByIdAsync(aggregateId), Times.Once);
        _mockEventBus.Verify(x => x.PublishEventsAsync(It.IsAny<IEnumerable<IIntegrationEvent>>()), Times.Once);

        #endregion
    }
}