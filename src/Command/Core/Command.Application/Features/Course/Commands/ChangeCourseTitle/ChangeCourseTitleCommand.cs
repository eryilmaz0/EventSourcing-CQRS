﻿using MediatR;

namespace Command.Application.Features.Course.Commands.ChangeCourseTitle;

public class ChangeCourseTitleCommand : IRequest<ChangeCourseTitleResponse>
{
    public Guid CourseId { get; set; }
    public string Title { get; set; }
}