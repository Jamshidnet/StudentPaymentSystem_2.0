using AutoMapper;
using MediatR;
using StudentPaymentSystem.Application.Common.Exceptions;
using StudentPaymentSystem.Application.Common.Interfaces;
using StudentPaymentSystem.Application.UseCases.Courses.Models;
using StudentPaymentSystem.Domein.Entities;

namespace StudentPaymentSystem.Application.UseCases.Courses.Queries.GetCourseQuery;

public  record GetCourseQuery(Guid Id) : IRequest<CourseDto>;

public class GetCourseQueryHandler : IRequestHandler<GetCourseQuery, CourseDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public GetCourseQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<CourseDto> Handle(GetCourseQuery request, CancellationToken cancellationToken)
    {
        Course course = FilterIfCourseExsists(request.Id);

          return  _mapper.Map<CourseDto>(course);
    }

    private Course FilterIfCourseExsists(Guid id)
    {
        Course? course = _dbContext.Courses.FirstOrDefault(x => x.Id == id);

        if(course is null)
        {
            throw new NotFoundException(" There is on course with this Id. ");
        }

        return course;
    }
}

