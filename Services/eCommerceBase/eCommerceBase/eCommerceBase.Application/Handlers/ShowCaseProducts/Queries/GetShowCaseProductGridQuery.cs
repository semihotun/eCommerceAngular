using MediatR;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Domain.Result;
using eCommerceBase.Application.Handlers.ShowCaseProducts.Queries.Dtos;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.Context;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;
using Microsoft.EntityFrameworkCore;

namespace eCommerceBase.Application.Handlers.ShowCaseProducts.Queries;
public record GetShowCaseProductGridQuery(Guid ShowCaseId, int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList)
    : IRequest<Result<PagedList<ShowCaseProductGrid>>>;
public class GetShowCaseProductGridQueryHandler(CoreDbContext coreDbContext, ICacheService cacheService)
    : IRequestHandler<GetShowCaseProductGridQuery, Result<PagedList<ShowCaseProductGrid>>>
{
    private readonly CoreDbContext _coreDbContext = coreDbContext;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<ShowCaseProductGrid>>> Handle(GetShowCaseProductGridQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync<Result<PagedList<ShowCaseProductGrid>>>(request, async () =>
        {
            var query = await _coreDbContext.Query<ShowCaseProduct>()
            .Include(x => x.Product)
            .Where(x => x.ShowCaseId == request.ShowCaseId)
            .Select(x => new ShowCaseProductGrid
            {
                Id = x.Id,
                ProductId = x.Product!.Id,
                ProductName = x.Product!.ProductName
            }).ToTableSettings(new PagedListFilterModel()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                FilterModelList = request.FilterModelList,
                OrderByColumnName = request.OrderByColumnName
            });
            return Result.SuccessDataResult<PagedList<ShowCaseProductGrid>>(query);
        }, cancellationToken);
    }
}