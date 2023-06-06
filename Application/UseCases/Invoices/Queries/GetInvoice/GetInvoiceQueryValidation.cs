using FluentValidation;

namespace StudentPaymentSystem.Application.UseCases.Invoices.Queries.GetInvoice;

public class GetInvoiceQueryValidation : AbstractValidator<GetInvoiceQuery>
{
    public GetInvoiceQueryValidation()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual((Guid)default).WithMessage(" invalid Guid Format. ");
    }
}

