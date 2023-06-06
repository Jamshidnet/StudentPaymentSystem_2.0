using AutoMapper;
using MediatR;
using StudentPaymentSystem.Application.Common.Exceptions;
using StudentPaymentSystem.Application.Common.Interfaces;
using StudentPaymentSystem.Application.UseCases.Payments.Models;
using StudentPaymentSystem.Domein.Entities;

namespace StudentPaymentSystem.Application.UseCases.Payments.Commands.DeletePayment;


public record DeletePaymentCommand(Guid Id) : IRequest<PaymentDto>;

public class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand, PaymentDto>
{
    private IApplicationDbContext _dbContext;
    private IMapper _mapper;

    public DeletePaymentCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PaymentDto> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
    {
        Payment payment = FilterIfPaymentExsists(request.Id);

        _dbContext.Payments.Remove(payment);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PaymentDto>(payment);
    }

    private Payment FilterIfPaymentExsists(Guid id)
    {
        Payment? payment = _dbContext.Payments.FirstOrDefault(c => c.Id == id);

        if (payment is null)
        {
            throw new NotFoundException(" There is no payment with id. ");
        }

        return payment;
    }
}

