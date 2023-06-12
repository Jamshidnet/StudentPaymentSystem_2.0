using LazyCache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StudentPaymentSystem.Application.Common.Models;
using StudentPaymentSystem.Application.UseCases.Payments.Commands.CreatePayment;
using StudentPaymentSystem.Application.UseCases.Payments.Commands.DeletePayment;
using StudentPaymentSystem.Application.UseCases.Payments.Models;
using StudentPaymentSystem.Application.UseCases.Payments.Queries.GetAllPayment;
using StudentPaymentSystem.Application.UseCases.Payments.Queries.GetPayment;

namespace StudentPaymentSystem_2._0.Controllers;

public class PaymentController : ApiBaseController
{
    public PaymentController(IAppCache appCache)
    {
        _appCache = appCache;
    }
    [HttpPost]
    public async ValueTask<ActionResult<PaymentDto>> PaymentPaymentAsync(CreatePaymentCommand command)
    {
        PaymentDto dto = await Mediator.Send(command);

        return Ok(dto);
    }

    [HttpGet("{paymentId}")]
    public async ValueTask<ActionResult<PaymentDto>> GetPaymentAsync(Guid paymentId)
    {
        return await Mediator.Send(new GetPaymentQuery(paymentId));
    }

    [HttpGet]
    public async ValueTask<ActionResult<PaginatedList<PaymentDto>>> GetPaymentsWithPaginated([FromQuery] GetallPaymentQuery query)
    {
        return await _appCache.GetOrAddAsync(My_Key,
          async x =>
          {
              x.SetAbsoluteExpiration(TimeSpan.FromSeconds(20));
              return Ok(await Mediator.Send(query));
          });
    }

    [HttpDelete("{paymentId}")]
    public async ValueTask<ActionResult<PaymentDto>> DeletePaymentAsync(Guid paymentId)
    {
        return await Mediator.Send(new DeletePaymentCommand(paymentId));
    }
}
