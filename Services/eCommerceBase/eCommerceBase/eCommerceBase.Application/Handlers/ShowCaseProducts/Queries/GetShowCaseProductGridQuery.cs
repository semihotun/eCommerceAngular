using eCommerceBase.Application.Handlers.ShowCaseProducts.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Persistence.GenericRepository;
using MediatR;

namespace eCommerceBase.Application.Handlers.ShowCaseProducts.Queries;
public record GetShowCaseProductGridQuery(Guid ShowCaseId, int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList)
    : IRequest<Result<PagedList<ShowCaseProductGrid>>>;
public class GetShowCaseProductGridQueryHandler(IReadDbRepository<ShowCaseProduct> showCaseProductRepository, ICacheService cacheService)
    : IRequestHandler<GetShowCaseProductGridQuery, Result<PagedList<ShowCaseProductGrid>>>
{
    private readonly IReadDbRepository<ShowCaseProduct> _showCaseProductRepository = showCaseProductRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<ShowCaseProductGrid>>> Handle(GetShowCaseProductGridQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request, async () =>
        {
            var query = await _showCaseProductRepository.Query()
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
            return Result.SuccessDataResult(query);
        }, cancellationToken);
    }
}