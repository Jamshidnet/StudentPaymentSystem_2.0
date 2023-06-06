using AutoMapper;
using MediatR;
using StudentPaymentSystem.Application.Common.Exceptions;
using StudentPaymentSystem.Application.Common.Interfaces;
using StudentPaymentSystem.Application.UseCases.Students.Models;
using StudentPaymentSystem.Domein.Entities;

namespace StudentPaymentSystem.Application.UseCases.Students.Queries.GetStudent;

public  record GetStudentQuery(Guid Id) : IRequest<StudentDto>;

public class GetStudentQueryHandler : IRequestHandler<GetStudentQuery, StudentDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public GetStudentQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<StudentDto> Handle(GetStudentQuery request, CancellationToken cancellationToken)
    {
        Student student = FilterIfStudentExsists(request.Id);

        return _mapper.Map<StudentDto>(student);
    }

    private Student FilterIfStudentExsists(Guid id)
    {
        Student? student = _dbContext.Students.FirstOrDefault(x => x.Id == id);

        if (student is null)
        {
            throw new NotFoundException(" There is on student with this Id. ");
        }

        return student;
    }


}