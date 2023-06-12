using AutoMapper;
using MediatR;
using StudentPaymentSystem.Application.Common.Interfaces;
using StudentPaymentSystem.Application.UseCases.Payments.Models;
using StudentPaymentSystem.Domein.Entities;

namespace StudentPaymentSystem.Application.UseCases.Payments.Commands.CreatePayment;


public record CreatePaymentCommand : IRequest<PaymentDto>
{
    public Guid StudentID { get; set; }

    public DateTime PaymentDate { get; set; }

    public decimal Amount { get; set; }

    public Guid InvoiceId { get; set; }

}
public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, PaymentDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public CreatePaymentCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PaymentDto> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {


        Payment payment = new Payment()
        {
         StudentID= request.StudentID,
         PaymentDate= request.PaymentDate,
         Amount= request.Amount,
         InvoiceId= request.InvoiceId
        };

        await _dbContext.Payments.AddAsync(payment);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PaymentDto>(payment);
    }
}
