using StudentPaymentSystem.Application.UseCases.Payments.Models;

namespace StudentPaymentSystem.Application.UseCases.Invoices.Models;

public class GetAllInvoiceDto : InvoiceDto
{
    public ICollection<PaymentDto> Payments { get; set; }
}
