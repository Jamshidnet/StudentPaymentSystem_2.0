using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using StudentPaymentSystem.Application.Common.Exceptions;
using StudentPaymentSystem.Application.Common.Interfaces;
using StudentPaymentSystem.Application.UseCases.Students.Commands.CreateStudent;
using StudentPaymentSystem.Application.UseCases.Students.Models;
using StudentPaymentSystem.Domein.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPaymentSystem.Application.UseCases.Students.Commands.UpdateStudent
{
    public  class UpdateStudentCommand : IRequest<StudentDto>
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

    }
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, StudentDto>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public UpdateStudentCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<StudentDto> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {

            Student? student = _dbContext.Students.Find(request.Id);

            if(student is null)
            {
                throw new NotFoundException(" There is no student with this Id. ");
            }
            student.PhoneNumber = request.PhoneNumber;
            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            student.Email = request.Email;
            student.Address = request.Address;


            await _dbContext.UpdateAsync(student);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<StudentDto>(student);
        }

    }
}
