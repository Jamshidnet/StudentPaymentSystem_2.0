using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPaymentSystem.Application.UseCases.Payments.Commands.CreatePayment
{
    public  class CreatePaymentCommandValidation : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidation()
        {
            RuleFor(x => x.InvoiceId).NotEqual((Guid)default).WithMessage(" Guid is incorrect format. ");
            RuleFor(x => x.StudentID).NotEqual((Guid)default).WithMessage(" Guid is incorrect format. ");
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage(" the amount must be higher than zero. ");
        }
    }
}
