using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentPaymentSystem_2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        private IMediator? _mediator;
        public IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

        public IAppCache? _appCache;

        protected readonly string My_Key = "ThiS iS KeY";
    }
}
