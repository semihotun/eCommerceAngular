using eCommerceBase.Application.Handlers.Products.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerceBase.Application.Handlers.Products.Queries;
public record GetProductDTOByIdQuery(System.Guid? Id) : IRequest<Result<ProductDto>>;
public class GetProductDTOByIdQueryHandler(CoreDbContext coreDbContext,
        ICacheService cacheService) : IRequestHandler<GetProductDTOByIdQuery,
		Result<ProductDto>>
{
    private readonly CoreDbContext _coreDbContext = coreDbContext;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<ProductDto>> Handle(GetProductDTOByIdQuery request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var data = await _coreDbContext.Query<Product>()
                   .Select(ProductQueryExtensions.ToProductDto)
                   .FirstOrDefaultAsync(x => x.Id == request.Id);
            return Result.SuccessDataResult<ProductDto>(data!);
        },
		cancellationToken);
    }
}