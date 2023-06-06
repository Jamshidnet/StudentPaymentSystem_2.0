using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentPaymentSystem.Application.Common.Interfaces;
using StudentPaymentSystem.Application.Common.Models;
using StudentPaymentSystem.Application.UseCases.Invoices.Models;
using StudentPaymentSystem.Domein.Entities;

namespace StudentPaymentSystem.Application.UseCases.Invoices.Queries.GetAllInvoice;


public record GetAllInvoiceQuery
: IRequest<PaginatedList<InvoiceDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetallInvoiceCommmandHandler : IRequestHandler<GetAllInvoiceQuery, PaginatedList<InvoiceDto>>
{

    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public GetallInvoiceCommmandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PaginatedList<InvoiceDto>> Handle(GetAllInvoiceQuery request, CancellationToken cancellationToken)
    {
        Invoice[] orders = await _dbContext.Invoices.Include(x => x.Payments).ToArrayAsync();

        List<InvoiceDto> dtos = _mapper.Map<InvoiceDto[]>(orders).ToList();

        PaginatedList<InvoiceDto> paginatedList =
             PaginatedList<InvoiceDto>.CreateAsync(
                dtos, request.PageNumber, request.PageSize);

        return paginatedList;
    }
}