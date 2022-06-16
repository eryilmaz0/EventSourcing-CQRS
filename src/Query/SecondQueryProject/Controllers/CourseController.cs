using Microsoft.AspNetCore.Mvc;
using SecondQueryProject.Abstract.Repository;
using SecondQueryProject.ReadModel;

namespace SecondQueryProject.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class CourseController : ControllerBase
{
    private readonly IRepository<Course> _repository;

    public CourseController(IRepository<Course> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    
    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        return Ok(await _repository.GetAll());
    }


    [HttpGet]
    [Route("{courseId}")]
    public async Task<IActionResult> GetCourse(string courseId)
    {
        return Ok(await _repository.Get(x => x.Id == courseId));
    }
}