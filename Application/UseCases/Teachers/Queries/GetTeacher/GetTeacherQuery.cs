using AutoMapper;
using MediatR;
using StudentPaymentSystem.Application.Common.Exceptions;
using StudentPaymentSystem.Application.Common.Interfaces;
using StudentPaymentSystem.Application.UseCases.Teachers.Models;
using StudentPaymentSystem.Domein.Entities;

namespace StudentPaymentSystem.Application.UseCases.Teachers.Queries.GetTeacher;


public record GetTeacherQuery(Guid Id) : IRequest<TeacherDto>;

public class GetTeacherQueryHandler : IRequestHandler<GetTeacherQuery, TeacherDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public GetTeacherQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<TeacherDto> Handle(GetTeacherQuery request, CancellationToken cancellationToken)
    {
        Teacher teacher = FilterIfTeacherExsists(request.Id);

        return _mapper.Map<TeacherDto>(teacher);
    }

    private Teacher FilterIfTeacherExsists(Guid id)
    {
        Teacher? teacher = _dbContext.Teachers.FirstOrDefault(x => x.Id == id);

        if (teacher is null)
        {
            throw new NotFoundException(" There is no teacher with this Id. ");
        }

        return teacher;
    }
}


