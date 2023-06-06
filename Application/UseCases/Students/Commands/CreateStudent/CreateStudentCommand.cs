using AutoMapper;
using MediatR;
using StudentPaymentSystem.Application.Common.Exceptions;
using StudentPaymentSystem.Application.Common.Interfaces;
using StudentPaymentSystem.Application.UseCases.Students.Models;
using StudentPaymentSystem.Domein.Entities;

namespace StudentPaymentSystem.Application.UseCases.Students.Commands.CreateStudent;

public class CreateStudentCommand : IRequest<StudentDto>
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

}
public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, StudentDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public CreateStudentCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<StudentDto> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {

        FilterIfStudentExsists(request.PhoneNumber);

        Student student = new Student()
        {
          FirstName=request.FirstName,

          LastName=request.LastName,

          Email=request.Email,
          Address=request.Address,
          PhoneNumber=request.PhoneNumber
          
        };

        await _dbContext.AddAsync(student);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<StudentDto>(student);
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
