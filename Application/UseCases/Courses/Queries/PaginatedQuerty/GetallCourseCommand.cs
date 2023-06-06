using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentPaymentSystem.Application.Common.Interfaces;
using StudentPaymentSystem.Application.Common.Models;
using StudentPaymentSystem.Application.UseCases.Courses.Models;
using StudentPaymentSystem.Domein.Entities;

namespace StudentPaymentSystem.Application.UseCases.Courses.Queries.PaginatedQuerty;

public record GetallCourseQuery
: IRequest<PaginatedList<CourseDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetallCourseCommmandHandler : IRequestHandler<GetallCourseQuery, PaginatedList<CourseDto>>
{

    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public GetallCourseCommmandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CourseDto>> Handle(GetallCourseQuery request, CancellationToken cancellationToken)
    {
        Course[] orders = await _dbContext.Courses.Include(x => x.Students).ToArrayAsync();

        List<CourseDto> dtos = _mapper.Map<CourseDto[]>(orders).ToList();

        PaginatedList<CourseDto> paginatedList =
             PaginatedList<CourseDto>.CreateAsync(
                dtos, request.PageNumber, request.PageSize);

        return paginatedList;
    }
}
 
