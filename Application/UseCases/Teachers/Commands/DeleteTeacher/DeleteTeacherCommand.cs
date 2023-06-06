using AutoMapper;
using MediatR;
using StudentPaymentSystem.Application.Common.Exceptions;
using StudentPaymentSystem.Application.Common.Interfaces;
using StudentPaymentSystem.Application.UseCases.Teachers.Models;
using StudentPaymentSystem.Domein.Entities;

namespace StudentPaymentSystem.Application.UseCases.Teachers.Commands.DeleteTeacher;


public record DeleteTeacherCommand(Guid Id) : IRequest<TeacherDto>;

public class DeleteTeacherCommandHandler : IRequestHandler<DeleteTeacherCommand, TeacherDto>
{
    private IApplicationDbContext _dbContext;
    private IMapper _mapper;

    public DeleteTeacherCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<TeacherDto> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
    {
        Teacher teacher = FilterIfTeacherExsists(request.Id);

        _dbContext.Teachers.Remove(teacher);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TeacherDto>(teacher);
    }

    private Teacher FilterIfTeacherExsists(Guid id)
    {
        Teacher? teacher = _dbContext.Teachers.FirstOrDefault(c => c.Id == id);

        if (teacher is null)
        {
            throw new NotFoundException(" There is no teacher with id. ");
        }

        return teacher;
    }
}

