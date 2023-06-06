using AutoMapper;
using MediatR;
using StudentPaymentSystem.Application.Common.Exceptions;
using StudentPaymentSystem.Application.Common.Interfaces;
using StudentPaymentSystem.Application.UseCases.Courses.Models;
using StudentPaymentSystem.Domein.Entities;

namespace StudentPaymentSystem.Application.UseCases.Courses.Commands.UpdateCourse;

public  record UpdateCourseCommand : IRequest<CourseDto>
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }

    public Guid TeacherId { get; set; }

    public decimal Fee { get; set; }
}

public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, CourseDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public UpdateCourseCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<CourseDto> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        FilterIfCourseExsists(request.Name);

        Course course = new Course()
        {
            Name = request.Name,
            Description = request.Description,
            TeacherId = request.TeacherId,
            Fee = request.Fee
        };

        await _dbContext.UpdateAsync(course);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CourseDto>(course);
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
