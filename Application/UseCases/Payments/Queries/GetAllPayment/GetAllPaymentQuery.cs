using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentPaymentSystem.Application.Common.Interfaces;
using StudentPaymentSystem.Application.Common.Models;
using StudentPaymentSystem.Application.UseCases.Payments.Models;
using StudentPaymentSystem.Domein.Entities;

namespace StudentPaymentSystem.Application.UseCases.Payments.Queries.GetAllPayment;


public record GetallPaymentQuery
: IRequest<PaginatedList<PaymentDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetallPaymentCommmandHandler : IRequestHandler<GetallPaymentQuery, PaginatedList<PaymentDto>>
{

    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public GetallPaymentCommmandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PaginatedList<PaymentDto>> Handle(GetallPaymentQuery request, CancellationToken cancellationToken)
    {
        Payment[] orders = await _dbContext.Payments.ToArrayAsync();

        List<PaymentDto> dtos = _mapper.Map<PaymentDto[]>(orders).ToList();

        PaginatedList<PaymentDto> paginatedList =
             PaginatedList<PaymentDto>.CreateAsync(
                dtos, request.PageNumber, request.PageSize);

        return paginatedList;
    }
}
