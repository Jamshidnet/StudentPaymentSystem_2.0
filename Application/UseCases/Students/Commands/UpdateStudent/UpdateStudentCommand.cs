using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentPaymentSystem.Application.Common.Exceptions;
using StudentPaymentSystem.Application.Common.Interfaces;
using StudentPaymentSystem.Application.UseCases.Students.Models;
using StudentPaymentSystem.Domein.Entities;

namespace StudentPaymentSystem.Application.UseCases.Students.Commands.UpdateStudent;

public class UpdateStudentCommand : IRequest<GetAllStudentDto>
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }

    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public ICollection<Guid> CourseIds { get; set; }

}
public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, GetAllStudentDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public UpdateStudentCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<GetAllStudentDto> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        Student student = await FilterIfStudentExsists(request.Id);
        IEnumerable<Course> courses = FilterifCourseIdsAreAvialible(request.CourseIds);

        student.FirstName = request.FirstName;
        student.LastName = request.LastName;
        student.Address = request.Address;
        student.Email = request.Email;
        student.PhoneNumber = request.PhoneNumber;
        student.Courses = courses.ToList();
        _dbContext.Students.Update(student);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<GetAllStudentDto>(student);
    }

    private IEnumerable<Course> FilterifCourseIdsAreAvialible(ICollection<Guid> studentIds)
    {
        foreach (var Id in studentIds)
            yield return _dbContext.Courses.Find(Id)
                ?? throw new NotFoundException($" there is no course with this {Id} id. ");
    }

    private async Task<Student> FilterIfStudentExsists(Guid id)
    {
        Student? student = await _dbContext.Students.Include("Courses")
            .FirstOrDefaultAsync(x => x.Id == id);

        return student
            ?? throw new NotFoundException(
                " there is no student with this id. ");
    }
}
