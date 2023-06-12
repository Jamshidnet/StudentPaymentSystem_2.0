using AutoMapper;
using MediatR;
using StudentPaymentSystem.Application.Common.Exceptions;
using StudentPaymentSystem.Application.Common.Interfaces;
using StudentPaymentSystem.Application.UseCases.Teachers.Models;
using StudentPaymentSystem.Domein.Entities;

namespace StudentPaymentSystem.Application.UseCases.Teachers.Commands.UpdateTeacher;

public  class UpdateTeacherCommand : IRequest<TeacherDto>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class UpdateTeacherCommandHandler : IRequestHandler<UpdateTeacherCommand, TeacherDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public UpdateTeacherCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<TeacherDto> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
    {

       Teacher teacher=  FilterIfTeacherExsists(request.Id);

        teacher.FirstName = request.FirstName;
        teacher.LastName = request.LastName;

         _dbContext.Teachers.Update(teacher);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TeacherDto>(teacher);
    }

    private Teacher FilterIfTeacherExsists(Guid Id)
    {
        Teacher? teacher = _dbContext.Teachers.FirstOrDefault(x => x.Id==Id);

        if (teacher is null)
        {
            throw new NotFoundException(" There is no   teacher with this Id . ");
        }

        return teacher;
    }
}
