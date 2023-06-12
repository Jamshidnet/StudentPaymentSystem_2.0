using LazyCache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StudentPaymentSystem.Application.Common.Models;
using StudentPaymentSystem.Application.UseCases.Students.Commands.CreateStudent;
using StudentPaymentSystem.Application.UseCases.Students.Commands.DeleteStudent;
using StudentPaymentSystem.Application.UseCases.Students.Commands.UpdateStudent;
using StudentPaymentSystem.Application.UseCases.Students.Models;
using StudentPaymentSystem.Application.UseCases.Students.Queries.GetStudent;
using StudentPaymentSystem.Application.UseCases.Students.Queries.PaginatedStudentQuery;

namespace StudentPaymentSystem_2._0.Controllers;

public class StudentController : ApiBaseController
{
    public StudentController(IAppCache appCache)
    {
        _appCache = appCache;
    }

    [HttpPost]
    public async ValueTask<ActionResult<GetAllStudentDto>> StudentStudentAsync(CreateStudentCommand command)
    {
        GetAllStudentDto dto = await Mediator.Send(command);

        return Ok(dto);
    }

    [HttpGet("{studentId}")]
    public async ValueTask<ActionResult<GetAllStudentDto>> GetStudentAsync(Guid studentId)
    {
        return await Mediator.Send(new GetStudentQuery(studentId));
    }

    [HttpGet]
    public async ValueTask<ActionResult<PaginatedList<StudentDto>>> GetStudentsWithPaginated([FromQuery] GetAllStudentQuery query)
    {
        return await _appCache.GetOrAddAsync(My_Key,
           async x =>
           {
               x.SetAbsoluteExpiration(TimeSpan.FromSeconds(20));
               return Ok(await Mediator.Send(query));
           });
    }

    [HttpPut]
    public async ValueTask<ActionResult<GetAllStudentDto>> PutStudentAsync(UpdateStudentCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete("{studentId}")]
    public async ValueTask<ActionResult<GetAllStudentDto>> DeleteStudentAsync(Guid studentId)
    {
        return await Mediator.Send(new DeleteStudentCommand(studentId));
    }

}
