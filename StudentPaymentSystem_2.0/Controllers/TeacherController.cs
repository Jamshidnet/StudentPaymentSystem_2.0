﻿using LazyCache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StudentPaymentSystem.Application.Common.Models;
using StudentPaymentSystem.Application.UseCases.Teachers.Commands.CreateTeacher;
using StudentPaymentSystem.Application.UseCases.Teachers.Commands.DeleteTeacher;
using StudentPaymentSystem.Application.UseCases.Teachers.Commands.UpdateTeacher;
using StudentPaymentSystem.Application.UseCases.Teachers.Models;
using StudentPaymentSystem.Application.UseCases.Teachers.Queries.GetAllTeachers;
using StudentPaymentSystem.Application.UseCases.Teachers.Queries.GetTeacher;

namespace StudentPaymentSystem_2._0.Controllers
{
    public class TeacherController : ApiBaseController
    {
        public TeacherController(IAppCache appCache, IConfiguration configuration)
        {
            _appCache = appCache;
            _configuration = configuration;
        }

        [HttpPost]
        public async ValueTask<ActionResult<TeacherDto>> TeacherTeacherAsync(CreateTeacherCommand command)
        {
            TeacherDto dto = await Mediator.Send(command);

            return Ok(dto);
        }

        [HttpGet("{teacherId}")]
        public async ValueTask<ActionResult<TeacherDto>> GetTeacherAsync(Guid teacherId)
        {
            return await Mediator.Send(new GetTeacherQuery(teacherId));
        }

        [HttpGet]
        public async ValueTask<ActionResult<PaginatedList<TeacherDto>>> GetTeachersWithPaginated([FromQuery] GetallTeacherQuery query)
        {
            return await _appCache.GetOrAddAsync(_configuration?.GetValue<string>("TeacherKeyForLazyCache"),
          async x =>
          {
              x.SetAbsoluteExpiration(TimeSpan.FromSeconds(20));
              return Ok(await Mediator.Send(query));
          });
        }

        [HttpPut]
        public async ValueTask<ActionResult<TeacherDto>> PutTeacherAsync(UpdateTeacherCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{teacherId}")]
        public async ValueTask<ActionResult<TeacherDto>> DeleteTeacherAsync(Guid teacherId)
        {
            return await Mediator.Send(new DeleteTeacherCommand(teacherId));
        }
    }
}
