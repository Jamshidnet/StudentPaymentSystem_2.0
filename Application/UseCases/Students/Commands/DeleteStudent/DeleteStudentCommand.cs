using AutoMapper;
using MediatR;
using StudentPaymentSystem.Application.Common.Exceptions;
using StudentPaymentSystem.Application.Common.Interfaces;
using StudentPaymentSystem.Application.UseCases.Students.Models;
using StudentPaymentSystem.Domein.Entities;

namespace StudentPaymentSystem.Application.UseCases.Students.Commands.DeleteStudent;

public record DeleteStudentCommand(Guid Id) : IRequest<StudentDto>;

public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, StudentDto>
{
    private IApplicationDbContext _dbContext;
    private IMapper _mapper;

    public DeleteStudentCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<StudentDto> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        Student student = FilterIfStudentExsists(request.Id);

        _dbContext.Students.Remove(student);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<StudentDto>(student);
    }

    private Student FilterIfStudentExsists(Guid id)
    {
        Student? student = _dbContext.Students.FirstOrDefault(c => c.Id == id);

        if (student is null)
        {
            throw new NotFoundException(" There is no student with id. ");
        }

        return student;
    }
}


