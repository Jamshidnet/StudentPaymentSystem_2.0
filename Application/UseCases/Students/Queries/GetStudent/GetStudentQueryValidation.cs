using FluentValidation;
using StudentPaymentSystem.Application.UseCases.Students.Models;

namespace StudentPaymentSystem.Application.UseCases.Students.Queries.GetStudent;

public  class GetStudentQueryValidation : AbstractValidator<StudentDto>
{
    public GetStudentQueryValidation()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual((Guid)default).WithMessage(" invalid Guid Format. ");
    }
}
