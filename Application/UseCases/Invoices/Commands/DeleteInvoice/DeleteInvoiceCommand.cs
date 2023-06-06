using AutoMapper;
using MediatR;
using StudentPaymentSystem.Application.Common.Exceptions;
using StudentPaymentSystem.Application.Common.Interfaces;
using StudentPaymentSystem.Application.UseCases.Invoices.Models;
using StudentPaymentSystem.Domein.Entities;

namespace StudentPaymentSystem.Application.UseCases.Invoices.Commands.DeleteInvoice;


public record DeleteInvoiceCommand(Guid Id) : IRequest<InvoiceDto>;

public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand, InvoiceDto>
{
    private IApplicationDbContext _dbContext;
    private IMapper _mapper;

    public DeleteInvoiceCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<InvoiceDto> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
    {
        Invoice invoice = FilterIfInvoiceExsists(request.Id);

        _dbContext.Invoices.Remove(invoice);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<InvoiceDto>(invoice);
    }

    private Invoice FilterIfInvoiceExsists(Guid id)
    {
        Invoice? invoice = _dbContext.Invoices.FirstOrDefault(c => c.Id == id);

        if (invoice is null)
        {
            throw new NotFoundException(" There is no invoice with id. ");
        }

        return invoice;
    }
}

