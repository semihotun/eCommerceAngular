using MediatR;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Domain.Result;
using eCommerceBase.Application.Handlers.Products.Queries.Dtos;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.Context;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;
using Microsoft.EntityFrameworkCore;

namespace eCommerceBase.Application.Handlers.Products.Queries;
public record GetProductGridDTOQuery(int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList) : IRequest<Result<PagedList<ProductGridDTO>>>;
public class GetProductGridDTOQueryHandler(CoreDbContext coreDbContext, ICacheService cacheService) : IRequestHandler<GetProductGridDTOQuery, Result<PagedList<ProductGridDTO>>>
{
    private readonly CoreDbContext _coreDbContext = coreDbContext;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<ProductGridDTO>>> Handle(GetProductGridDTOQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync<Result<PagedList<ProductGridDTO>>>(request, async () =>
        {
            var query = await _coreDbContext.Query<Product>()
            .Include(x => x.Brand)
            .Include(x => x.Category)
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
            return Result.SuccessDataResult<PagedList<ProductGridDTO>>(query);
        }, cancellationToken);
    }
}