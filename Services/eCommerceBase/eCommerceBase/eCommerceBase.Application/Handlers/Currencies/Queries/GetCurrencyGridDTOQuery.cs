using MediatR;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Domain.Result;
using eCommerceBase.Application.Handlers.Currencies.Queries.Dtos;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.Context;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;

namespace eCommerceBase.Application.Handlers.Currencies.Queries;
public record GetCurrencyGridDTOQuery(int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList) 
    : IRequest<Result<PagedList<CurrencyGridDTO>>>;
public class GetCurrencyGridDTOQueryHandler(CoreDbContext coreDbContext, ICacheService cacheService) 
    : IRequestHandler<GetCurrencyGridDTOQuery, Result<PagedList<CurrencyGridDTO>>>
{
    private readonly CoreDbContext _coreDbContext = coreDbContext;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<CurrencyGridDTO>>> Handle(GetCurrencyGridDTOQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync<Result<PagedList<CurrencyGridDTO>>>(request, async () =>
        {
            var query = await _coreDbContext.Query<Currency>().Select(x => new CurrencyGridDTO
            {
                Id = x.Id,
                Symbol = x.Symbol,
                Code = x.Code,
                Name = x.Name
            }).ToTableSettings(new PagedListFilterModel()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                FilterModelList = request.FilterModelList,
                OrderByColumnName = request.OrderByColumnName
            });
            return Result.SuccessDataResult<PagedList<CurrencyGridDTO>>(query);
        }, cancellationToken);
    }
}