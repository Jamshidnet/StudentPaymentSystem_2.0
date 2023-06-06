using FluentValidation;

namespace StudentPaymentSystem.Application.UseCases.Students.Commands.CreateStudent;

public class CreateStudentCommandValidation : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentCommandValidation()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage(" Name is required . ");
    }
}
