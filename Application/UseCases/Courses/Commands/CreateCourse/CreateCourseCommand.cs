using AutoMapper;
using MediatR;
using StudentPaymentSystem.Application.Common.Exceptions;
using StudentPaymentSystem.Application.Common.Interfaces;
using StudentPaymentSystem.Application.UseCases.Courses.Models;
using StudentPaymentSystem.Application.UseCases.Students.Models;
using StudentPaymentSystem.Domein.Entities;

namespace StudentPaymentSystem.Application.UseCases.Courses.Commands.CreateCourse;

public record CreateCourseCommand : IRequest<GetallCourseDto>
{
    public string Name { get; set; }

    public string Description { get; set; }

    public Guid TeacherId { get; set; }

    public decimal Fee { get; set; }
    public ICollection<Guid>? Students { get; set; }

}
public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, GetallCourseDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public CreateCourseCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<GetallCourseDto> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {

        FilterIfCourseExsists(request.Name);
        ICollection<Student>? students =  FilterIfAllStudentsExsists(request.Students);


        Course course = new Course()
        {
            Name= request.Name,
            Description=request.Description,
            TeacherId=request.TeacherId,
            Fee=request.Fee,
            Students=students
        };

        await _dbContext.Courses.AddAsync(course);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return  _mapper.Map<GetallCourseDto>(course);
    }

    private ICollection<Student>? FilterIfAllStudentsExsists(ICollection<Guid>? students)
    {
        List<Student> matchedStudents = new();
        if (students is null) return null;

        foreach (Guid Id in students)
        {
            var maybeStudent = _dbContext.Students.FirstOrDefault(s => s.Id == Id) 
                ?? throw new NotFoundException($" There is no Student with {Id} Id. ");
            matchedStudents.Add(maybeStudent);
        }

        return matchedStudents;
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
