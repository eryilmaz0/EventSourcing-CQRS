using FirstQueryProject.Abstract.Repository;
using FirstQueryProject.ReadModel;
using Microsoft.AspNetCore.Mvc;

namespace FirstQueryProject.Controllers;

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
        return Ok(await _repository.GetAllAsync());
    }


    [HttpGet]
    [Route("{courseId}")]
    public async Task<IActionResult> GetCourse(Guid courseId)
    {
        return Ok(await _repository.GetAsync(x => x.Id == courseId));
    }
}