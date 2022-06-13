using Command.API.ApiResponse;
using Command.Application.Abstracts.Infrastructure;
using Command.Application.Features.Course.Commands.ActivateCourse;
using Command.Application.Features.Course.Commands.AddSection;
using Command.Application.Features.Course.Commands.ChangeCourseDescription;
using Command.Application.Features.Course.Commands.ChangeCourseTitle;
using Command.Application.Features.Course.Commands.CommentCourse;
using Command.Application.Features.Course.Commands.CompleteCourse;
using Command.Application.Features.Course.Commands.CreateCourse;
using Command.Application.Features.Course.Commands.JoinCourse;
using Command.Application.Features.Course.Commands.LeaveCourse;
using Command.Application.Features.Course.Commands.PrePresentCourse;
using Microsoft.AspNetCore.Mvc;

namespace Command.API.Controllers;

[ApiController]
[Route("api/[Controller]/[Action]")]
public class CourseController : ControllerBase
{
    private readonly ICommandMediator _mediator;

    public CourseController(ICommandMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    
    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseCommand command)
    {
        var commandResult = await _mediator.SendAsync<CreateCourseCommand, CreateCourseResponse>(command);

        if (!commandResult.IsSuccess)
            return BadRequest(new ApiResponse<CreateCourseResponse>(command.TrackId, commandResult));

        return Ok(new ApiResponse<CreateCourseResponse>(command.TrackId, commandResult));
    }
    
    
    
    [HttpPost]
    public async Task<IActionResult> ChangeCourseTitle([FromBody] ChangeCourseTitleCommand command)
    {
        var commandResult = await _mediator.SendAsync<ChangeCourseTitleCommand, ChangeCourseTitleResponse>(command);

        if (!commandResult.IsSuccess)
            return BadRequest(new ApiResponse<ChangeCourseTitleResponse>(command.TrackId, commandResult));

        return Ok(new ApiResponse<ChangeCourseTitleResponse>(command.TrackId, commandResult));
    }
    
    
    
    [HttpPost]
    public async Task<IActionResult> ChangeCourseDescription([FromBody] ChangeCourseDescriptionCommand command)
    {
        var commandResult = await _mediator.SendAsync<ChangeCourseDescriptionCommand, ChangeCourseDescriptionResponse>(command);

        if (!commandResult.IsSuccess)
            return BadRequest(new ApiResponse<ChangeCourseDescriptionResponse>(command.TrackId, commandResult));

        return Ok(new ApiResponse<ChangeCourseDescriptionResponse>(command.TrackId, commandResult));
    }
    
    
    
    [HttpPost]
    public async Task<IActionResult> PrePresentCourse([FromBody] PrePresentCourseCommand command)
    {
        var commandResult = await _mediator.SendAsync<PrePresentCourseCommand, PrePresentCourseResponse>(command);

        if (!commandResult.IsSuccess)
            return BadRequest(new ApiResponse<PrePresentCourseResponse>(command.TrackId, commandResult));

        return Ok(new ApiResponse<PrePresentCourseResponse>(command.TrackId, commandResult));
    }
    
    
    
    [HttpPost]
    public async Task<IActionResult> ActivateCourse([FromBody] ActivateCourseCommand command)
    {
        var commandResult = await _mediator.SendAsync<ActivateCourseCommand, ActivateCourseResponse>(command);

        if (!commandResult.IsSuccess)
            return BadRequest(new ApiResponse<ActivateCourseResponse>(command.TrackId, commandResult));

        return Ok(new ApiResponse<ActivateCourseResponse>(command.TrackId, commandResult));
    }
    
    
    
    [HttpPost]
    public async Task<IActionResult> CompleteCourse([FromBody] CompleteCourseCommand command)
    {
        var commandResult = await _mediator.SendAsync<CompleteCourseCommand, CompleteCourseResponse>(command);

        if (!commandResult.IsSuccess)
            return BadRequest(new ApiResponse<CompleteCourseResponse>(command.TrackId, commandResult));

        return Ok(new ApiResponse<CompleteCourseResponse>(command.TrackId, commandResult));
    }
    
    
    
    [HttpPost]
    public async Task<IActionResult> AddSection([FromBody] AddSectionCommand command)
    {
        var commandResult = await _mediator.SendAsync<AddSectionCommand, AddSectionResponse>(command);

        if (!commandResult.IsSuccess)
            return BadRequest(new ApiResponse<AddSectionResponse>(command.TrackId, commandResult));

        return Ok(new ApiResponse<AddSectionResponse>(command.TrackId, commandResult));
    }
    
    
    
    [HttpPost]
    public async Task<IActionResult> JoinCourse([FromBody] JoinCourseCommand command)
    {
        var commandResult = await _mediator.SendAsync<JoinCourseCommand, JoinCourseResponse>(command);

        if (!commandResult.IsSuccess)
            return BadRequest(new ApiResponse<JoinCourseResponse>(command.TrackId, commandResult));

        return Ok(new ApiResponse<JoinCourseResponse>(command.TrackId, commandResult));
    }
    
    
    
    [HttpPost]
    public async Task<IActionResult> LeaveCourse([FromBody] LeaveCourseCommand command)
    {
        var commandResult = await _mediator.SendAsync<LeaveCourseCommand, LeaveCourseResponse>(command);

        if (!commandResult.IsSuccess)
            return BadRequest(new ApiResponse<LeaveCourseResponse>(command.TrackId, commandResult));

        return Ok(new ApiResponse<LeaveCourseResponse>(command.TrackId, commandResult));
    }
    
    
    
    [HttpPost]
    public async Task<IActionResult> CommentCourse([FromBody] CommentCourseCommand command)
    {
        var commandResult = await _mediator.SendAsync<CommentCourseCommand, CommentCourseResponse>(command);

        if (!commandResult.IsSuccess)
            return BadRequest(new ApiResponse<CommentCourseResponse>(command.TrackId, commandResult));

        return Ok(new ApiResponse<CommentCourseResponse>(command.TrackId, commandResult));
    }
    
}