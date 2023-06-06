using AutoMapper;
using MediatR;
using StudentPaymentSystem.Application.Common.Exceptions;
using StudentPaymentSystem.Application.Common.Interfaces;
using StudentPaymentSystem.Application.UseCases.Payments.Models;
using StudentPaymentSystem.Domein.Entities;

namespace StudentPaymentSystem.Application.UseCases.Payments.Queries.GetPayment;


public record GetPaymentQuery(Guid Id) : IRequest<PaymentDto>;

public class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, PaymentDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public GetPaymentQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<PaymentDto> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
    {
        Payment payment = FilterIfPaymentExsists(request.Id);

        return _mapper.Map<PaymentDto>(payment);
    }

    private Payment FilterIfPaymentExsists(Guid id)
    {
        Payment? payment = _dbContext.Payments.FirstOrDefault(x => x.Id == id);

        if (payment is null)
        {
            throw new NotFoundException(" There is no payment with this Id. ");
        }

        return payment;
    }
}

