using AutoMapper;
using MediatR;
using StudentPaymentSystem.Application.Common.Exceptions;
using StudentPaymentSystem.Application.Common.Interfaces;
using StudentPaymentSystem.Application.UseCases.Courses.Models;
using StudentPaymentSystem.Domein.Entities;

namespace StudentPaymentSystem.Application.UseCases.Courses.Commands.CreateCourse;

public record CreateCourseCommand : IRequest<CourseDto>
{
    public string Name { get; set; }

    public string Description { get; set; }

    public Guid TeacherId { get; set; }

    public decimal Fee { get; set; }

}
public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, CourseDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public CreateCourseCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<CourseDto> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {

        FilterIfCourseExsists(request.Name);

        Course course = new Course()
        {
            Name= request.Name,
            Description=request.Description,
            TeacherId=request.TeacherId,
            Fee=request.Fee
        };

        await _dbContext.AddAsync(course);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return  _mapper.Map<CourseDto>(course);
    }

    private void FilterIfCourseExsists(string name)
    {
        Course? course = _dbContext.Courses.FirstOrDefault(x => x.Name == name);

        if (course is not null)
        {
            throw new AlreadyExsistsException(" There is a  course with this name. Course name should be unique.  ");
        }
    }
}
