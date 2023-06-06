using FluentValidation;

namespace StudentPaymentSystem.Application.UseCases.Payments.Queries.GetPayment;

public class GetPaymentQueryValidation : AbstractValidator<GetPaymentQuery>
{
    public GetPaymentQueryValidation()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual((Guid)default).WithMessage(" invalid Guid Format. ");
    }
}
