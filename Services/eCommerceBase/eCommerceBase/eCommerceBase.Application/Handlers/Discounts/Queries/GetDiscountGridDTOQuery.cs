using eCommerceBase.Application.Handlers.Discounts.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Constant;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerceBase.Application.Handlers.Discounts.Queries;
public record GetDiscountGridDTOQuery(int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList) :
    IRequest<Result<PagedList<DiscountGridDTO>>>;
public class GetDiscountGridDTOQueryHandler(CoreDbContext coreDbContext, ICacheService cacheService) 
    : IRequestHandler<GetDiscountGridDTOQuery, Result<PagedList<DiscountGridDTO>>>
{
    private readonly CoreDbContext _coreDbContext = coreDbContext;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<DiscountGridDTO>>> Handle(GetDiscountGridDTOQuery request, CancellationToken cancellationToken)
    {
        //return await _cacheService.GetAsync<Result<PagedList<DiscountGridDTO>>>(request, async () =>
        //{
            var query = await _coreDbContext.Query<Discount>().Include(x => x.DiscountType)
            .Where(x=>x.DiscountTypeId != DiscountTypeConst.ProductCurrencyDiscount &&
                      x.DiscountTypeId != DiscountTypeConst.ProductPercentDiscount)
            .Include(x=>x.DiscountType)
            .Select(x => new DiscountGridDTO
            {
                Id = x.Id,
                Name = x.Name,
                DiscountTypeName = x.DiscountType!.Name
            }).ToTableSettings(new PagedListFilterModel()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                FilterModelList = request.FilterModelList,
                OrderByColumnName = request.OrderByColumnName
            });
            return Result.SuccessDataResult<PagedList<DiscountGridDTO>>(query);
        //}, cancellationToken);
    }
}