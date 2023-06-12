using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentPaymentSystem.Application.Common.Exceptions;
using StudentPaymentSystem.Application.Common.Interfaces;
using StudentPaymentSystem.Application.UseCases.Courses.Models;
using StudentPaymentSystem.Application.UseCases.Students.Models;
using StudentPaymentSystem.Domein.Entities;

namespace StudentPaymentSystem.Application.UseCases.Courses.Queries.GetCourseQuery;

public  record GetCourseQuery(Guid Id) : IRequest<GetallCourseDto>;

public class GetCourseQueryHandler : IRequestHandler<GetCourseQuery, GetallCourseDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public GetCourseQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<GetallCourseDto> Handle(GetCourseQuery request, CancellationToken cancellationToken)
    {
        GetallCourseDto course = FilterIfCourseExsists(request.Id);

          return  _mapper.Map<GetallCourseDto>(course);
    }

    private GetallCourseDto FilterIfCourseExsists(Guid id)
    {
        Course? course = _dbContext.Courses.Include(x=>x.Students).FirstOrDefault(x => x.Id == id);
        StudentDto[] mappedSt = _mapper.Map<StudentDto[]>(course.Students);
        GetallCourseDto getAllStudentDto = new()
        {
            Name = course.Name,
            Description = course.Description,
            Fee = course.Fee,
            Id = course.Id,
            TeacherId = course.TeacherId,
            Students = mappedSt
        };


        if (course is null)
        {
            throw new NotFoundException(" There is on course with this Id. ");
        }

        return getAllStudentDto;
    }
}

