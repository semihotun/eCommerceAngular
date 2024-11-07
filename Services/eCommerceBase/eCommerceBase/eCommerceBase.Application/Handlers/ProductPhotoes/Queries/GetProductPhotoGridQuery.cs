using MediatR;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Domain.Result;
using eCommerceBase.Application.Handlers.ProductPhotoes.Queries.Dtos;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.Context;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;

namespace eCommerceBase.Application.Handlers.ProductPhotoes.Queries;
public record GetProductPhotoGridByProductIdQuery(Guid ProductId,int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList)
    : IRequest<Result<PagedList<ProductPhotoGrid>>>;
public class GetProductPhotoGridQueryHandler(CoreDbContext coreDbContext, ICacheService cacheService)
    : IRequestHandler<GetProductPhotoGridByProductIdQuery, Result<PagedList<ProductPhotoGrid>>>
{
    private readonly CoreDbContext _coreDbContext = coreDbContext;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<ProductPhotoGrid>>> Handle(GetProductPhotoGridByProductIdQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync<Result<PagedList<ProductPhotoGrid>>>(request, async () =>
        {
            var query = await _coreDbContext.Query<ProductPhoto>()
            .Where(x=>x.ProductId == request.ProductId)
            .Select(x =>
            new ProductPhotoGrid
            {
                Id = x.Id,
                ProductId = x.ProductId,
                PhotoBase64 = x.PhotoBase64
            }).ToTableSettings(
                new PagedListFilterModel()
                {
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    FilterModelList = request.FilterModelList,
                    OrderByColumnName = request.OrderByColumnName
                });
            return Result.SuccessDataResult<PagedList<ProductPhotoGrid>>(query);
        }, cancellationToken);
    }
}