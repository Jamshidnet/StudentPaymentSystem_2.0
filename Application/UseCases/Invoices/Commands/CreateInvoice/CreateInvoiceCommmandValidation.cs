using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPaymentSystem.Application.UseCases.Invoices.Commands.CreateInvoice
{
    internal class CreateInvoiceCommmandValidation : AbstractValidator<CreateInvoiceCommand>
    {
        public CreateInvoiceCommmandValidation()
        {
            RuleFor(x => x.CourseID).NotEqual((Guid)default).WithMessage(" Guid is incorrect format. ");
            RuleFor(x => x.TotalAmount).GreaterThan(0).WithMessage(" the amount must be higher than zero. ");
        }
    }
}
