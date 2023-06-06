using AutoMapper;
using MediatR;
using StudentPaymentSystem.Application.Common.Exceptions;
using StudentPaymentSystem.Application.Common.Interfaces;
using StudentPaymentSystem.Application.UseCases.Invoices.Models;
using StudentPaymentSystem.Domein.Entities;

namespace StudentPaymentSystem.Application.UseCases.Invoices.Queries.GetInvoice;


public record GetInvoiceQuery(Guid Id) : IRequest<InvoiceDto>;

public class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, InvoiceDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public GetInvoiceQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<InvoiceDto> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
    {
        Invoice invoice = FilterIfInvoiceExsists(request.Id);

        return _mapper.Map<InvoiceDto>(invoice);
    }

    private Invoice FilterIfInvoiceExsists(Guid id)
    {
        Invoice? invoice = _dbContext.Invoices.FirstOrDefault(x => x.Id == id);

        if (invoice is null)
        {
            throw new NotFoundException(" There is no invoice with this Id. ");
        }

        return invoice;
    }
}


