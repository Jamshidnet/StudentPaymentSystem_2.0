using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentPaymentSystem.Application.Common.Exceptions;
using StudentPaymentSystem.Application.Common.Interfaces;
using StudentPaymentSystem.Application.UseCases.Courses.Models;
using StudentPaymentSystem.Application.UseCases.Payments.Models;
using StudentPaymentSystem.Application.UseCases.Students.Models;
using StudentPaymentSystem.Domein.Entities;

namespace StudentPaymentSystem.Application.UseCases.Students.Queries.GetStudent;

public  record GetStudentQuery(Guid Id) : IRequest<GetAllStudentDto>;

public class GetStudentQueryHandler : IRequestHandler<GetStudentQuery, GetAllStudentDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public GetStudentQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<GetAllStudentDto> Handle(GetStudentQuery request, CancellationToken cancellationToken)
    {
        GetAllStudentDto student = FilterIfStudentExsists(request.Id);

        return _mapper.Map<GetAllStudentDto>(student);
    }

    private GetAllStudentDto FilterIfStudentExsists(Guid id)
    {
        Student? student = _dbContext.Students
            .Include(x=>x.Courses)
            .Include(x=>x.Payments)
            .FirstOrDefault(x => x.Id == id);

        CourseDto[] mappedSt = _mapper.Map<CourseDto[]>(student.Courses);
        PaymentDto[] payments = _mapper.Map<PaymentDto[]>(student.Payments);
        GetStudentDtoWithPayments getAllStudentDto = new()
        {
            FirstName=student.FirstName,
            LastName=student.LastName,
            Address=student.Address,
            Id=student.Id,
            Email=student.Email,
            PhoneNumber = student.PhoneNumber,
            Courses=mappedSt,
            Payments=payments
        };
        

        if (student is null)
        {
            throw new NotFoundException(" There is on student with this Id. ");
        }

        return getAllStudentDto;
    }


}