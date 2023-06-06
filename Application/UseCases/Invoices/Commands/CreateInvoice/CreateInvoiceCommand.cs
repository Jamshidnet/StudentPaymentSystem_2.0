using AutoMapper;
using MediatR;
using StudentPaymentSystem.Application.Common.Interfaces;
using StudentPaymentSystem.Application.UseCases.Invoices.Models;
using StudentPaymentSystem.Domein.Entities;

namespace StudentPaymentSystem.Application.UseCases.Invoices.Commands.CreateInvoice;


public record CreateInvoiceCommand : IRequest<InvoiceDto>
{
    public Guid CourseID { get; set; }

    public DateTime IssueDate { get; set; }

    public decimal TotalAmount { get; set; }
}
public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, InvoiceDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public CreateInvoiceCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<InvoiceDto> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        
        Invoice invoice = new Invoice()
        {
           CourseID=request.CourseID,
           IssueDate=request.IssueDate,
           TotalAmount=request.TotalAmount
        };

        await _dbContext.AddAsync(invoice);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<InvoiceDto>(invoice);
    }
}

