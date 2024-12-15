using eCommerceBase.Application.Handlers.Products.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Persistence.GenericRepository;
using MediatR;

namespace eCommerceBase.Application.Handlers.Products.Queries;
public record GetProductGridDTOQuery(int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList) 
    : IRequest<Result<PagedList<ProductGridDTO>>>;
public class GetProductGridDTOQueryHandler(IReadDbRepository<Product> productRepository, ICacheService cacheService) 
    : IRequestHandler<GetProductGridDTOQuery, Result<PagedList<ProductGridDTO>>>
{
    private readonly IReadDbRepository<Product> _productRepository = productRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<ProductGridDTO>>> Handle(GetProductGridDTOQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request, async () =>
        {
            var query = await _productRepository.Query()
            .Select(x => new ProductGridDTO
            {
                Id = x.Id,
                CategoryName = x.Category!.CategoryName,
                BrandName = x.Brand!.BrandName,
                ProductName = x.ProductName
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