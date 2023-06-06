using Microsoft.AspNetCore.Mvc;
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
    [HttpPost]
    public async ValueTask<ActionResult<CourseDto>> CourseCourseAsync(CreateCourseCommand command)
    {
        CourseDto dto = await Mediator.Send(command);

        return Ok(dto);
    }

    [HttpGet("{courseId}")]
    public async ValueTask<ActionResult<CourseDto>> GetCourseAsync(Guid courseId)
    {
        return await Mediator.Send(new GetCourseQuery(courseId));
    }

    [HttpGet]
    public async ValueTask<ActionResult<PaginatedList<CourseDto>>> GetCoursesWithPaginated([FromQuery] GetallCourseQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPut]
    public async ValueTask<ActionResult<CourseDto>> PutCourseAsync(UpdateCourseCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete("{courseId}")]
    public async ValueTask<ActionResult<CourseDto>> DeleteCourseAsync(Guid courseId)
    {
        return await Mediator.Send(new DeleteCourseCommand(courseId));
    }
}
