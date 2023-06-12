using AutoMapper;
using MediatR;
using StudentPaymentSystem.Application.Common.Exceptions;
using StudentPaymentSystem.Application.Common.Interfaces;
using StudentPaymentSystem.Application.UseCases.Students.Models;
using StudentPaymentSystem.Domein.Entities;

namespace StudentPaymentSystem.Application.UseCases.Students.Commands.CreateStudent;

public class CreateStudentCommand : IRequest<GetAllStudentDto>
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public ICollection<Guid> Courses { get; set; }
}
public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, GetAllStudentDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public CreateStudentCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<GetAllStudentDto> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {

        FilterIfStudentExsists(request.PhoneNumber);
        ICollection<Course>? courses = FilterIfAllStudentsExsist(request.Courses);

        Student student = new ()
        {
          FirstName=request.FirstName,
          LastName=request.LastName,
          Email=request.Email,
          Address=request.Address,
          PhoneNumber=request.PhoneNumber,
          Courses=courses
        };

        await  _dbContext.Students.AddAsync(student);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<GetAllStudentDto>(student);
    }

    private ICollection<Course> FilterIfAllStudentsExsist(ICollection<Guid> courses)
    {
        List<Course> maybeCourses = new();
        foreach (Guid Id in courses)
        {
            var course = _dbContext.Courses.FirstOrDefault(c => c.Id == Id)
                ?? throw new NotFoundException($" There is no course with this {Id} id. ");
            maybeCourses.Add(course);
        }

        return maybeCourses;
    }

    private void FilterIfStudentExsists(string? PhoneNumber)
    {
        Student? student = _dbContext.Students.FirstOrDefault(x => x.PhoneNumber == PhoneNumber);

        if (student is not null)
        {
            throw new AlreadyExsistsException(" There is a  student with this phonenumber. Student should be unique.  ");
        }
    }
}
