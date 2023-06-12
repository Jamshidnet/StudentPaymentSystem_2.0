using Microsoft.AspNetCore.Mvc;
using StudentPaymentSystem.Application.Common.Models;
using StudentPaymentSystem.Application.UseCases.Invoices.Commands.CreateInvoice;
using StudentPaymentSystem.Application.UseCases.Invoices.Commands.DeleteInvoice;
using StudentPaymentSystem.Application.UseCases.Invoices.Models;
using StudentPaymentSystem.Application.UseCases.Invoices.Queries.GetAllInvoice;
using StudentPaymentSystem.Application.UseCases.Invoices.Queries.GetInvoice;

namespace StudentPaymentSystem_2._0.Controllers;

public class InvoiceController : ApiBaseController
{
    [HttpPost]
    public async ValueTask<ActionResult<InvoiceDto>> InvoiceInvoiceAsync(CreateInvoiceCommand command)
    {
        InvoiceDto dto = await Mediator.Send(command);

        return Ok(dto);
    }

    [HttpGet("{invoiceId}")]
    public async ValueTask<ActionResult<GetAllInvoiceDto>> GetInvoiceAsync(Guid invoiceId)
    {
        return await Mediator.Send(new GetInvoiceQuery(invoiceId));
    }

    [HttpGet]
    public async ValueTask<ActionResult<PaginatedList<InvoiceDto>>> GetInvoicesWithPaginated([FromQuery] GetAllInvoiceQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpDelete("{invoiceId}")]
    public async ValueTask<ActionResult<InvoiceDto>> DeleteInvoiceAsync(Guid invoiceId)
    {
        return await Mediator.Send(new DeleteInvoiceCommand(invoiceId));
    }
}
