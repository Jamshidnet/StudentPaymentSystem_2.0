using FluentValidation;

namespace StudentPaymentSystem.Application.UseCases.Teachers.Queries.GetTeacher;

public class GetTeacherQueryValidation : AbstractValidator<GetTeacherQuery>
{
    public GetTeacherQueryValidation()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual((Guid)default).WithMessage(" invalid Guid Format. ");
    }
}

