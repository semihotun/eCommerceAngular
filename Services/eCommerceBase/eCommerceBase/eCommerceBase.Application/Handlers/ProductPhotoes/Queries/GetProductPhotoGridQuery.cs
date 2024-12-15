using eCommerceBase.Application.Handlers.ProductPhotoes.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Persistence.GenericRepository;
using MediatR;

namespace eCommerceBase.Application.Handlers.ProductPhotoes.Queries;
public record GetProductPhotoGridByProductIdQuery(Guid ProductId,int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList)
    : IRequest<Result<PagedList<ProductPhotoGrid>>>;
public class GetProductPhotoGridQueryHandler(IReadDbRepository<ProductPhoto> productPhotoRepository, ICacheService cacheService)
    : IRequestHandler<GetProductPhotoGridByProductIdQuery, Result<PagedList<ProductPhotoGrid>>>
{
    private readonly IReadDbRepository<ProductPhoto> _productPhotoRepository = productPhotoRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<ProductPhotoGrid>>> Handle(GetProductPhotoGridByProductIdQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request, async () =>
        {
            var query = await _productPhotoRepository.Query()
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
            return Result.SuccessDataResult(query);
        }, cancellationToken);
    }
}