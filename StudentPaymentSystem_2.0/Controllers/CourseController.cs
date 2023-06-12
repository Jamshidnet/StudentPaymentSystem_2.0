using LazyCache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StudentPaymentSystem.Application.Common.Models;
using StudentPaymentSystem.Application.UseCases.Courses.Commands.CreateCourse;
using StudentPaymentSystem.Application.UseCases.Courses.Commands.DeleteCourse;
using StudentPaymentSystem.Application.UseCases.Courses.Commands.UpdateCourse;
using StudentPaymentSystem.Application.UseCases.Courses.Models;
using StudentPaymentSystem.Application.UseCases.Courses.Queries.GetCourseQuery;
using StudentPaymentSystem.Application.UseCases.Courses.Queries.PaginatedQuerty;

namespace StudentPaymentSystem_2._0.Controllers;

public class CourseController : ApiBaseController
{
    public CourseController(IAppCache appCache, IConfiguration configuration)
    {
        _appCache = appCache;
        _configuration = configuration;
    }

    [HttpPost]
    public async ValueTask<ActionResult<GetallCourseDto>> CourseCourseAsync(CreateCourseCommand command)
    {
        GetallCourseDto dto = await Mediator.Send(command);

        return Ok(dto);
    }

    [HttpGet("{courseId}")]
    public async ValueTask<ActionResult<GetallCourseDto>> GetCourseAsync(Guid courseId)
    {
        return await Mediator.Send(new GetCourseQuery(courseId));
    }

    [HttpGet]
    public async ValueTask<ActionResult<PaginatedList<CourseDto>>> GetCoursesWithPaginated([FromQuery] GetallCourseQuery query)
    {
        return await _appCache.GetOrAddAsync(_configuration?.GetValue<string>("CourseKeyForLazyCache"),
          async x =>
          {
              x.SetAbsoluteExpiration(TimeSpan.FromSeconds(20));
              return Ok(await Mediator.Send(query));
          });
    }

    [HttpPut]
    public async ValueTask<ActionResult<GetallCourseDto>> PutCourseAsync(UpdateCourseCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete("{courseId}")]
    public async ValueTask<ActionResult<GetallCourseDto>> DeleteCourseAsync(Guid courseId)
    {
        return await Mediator.Send(new DeleteCourseCommand(courseId));
    }
}
