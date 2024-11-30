using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Products.Extenison;
using eCommerceBase.Application.Handlers.Products.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerceBase.Application.Handlers.Products.Queries;
public record GetProductDTOBySlugQuery(string? Slug) : IRequest<Result<ProductDetailDTO>>;
public class GetProductDTOBySlugQueryHandler(CoreDbContext coreDbContext,
        ICacheService cacheService) : IRequestHandler<GetProductDTOBySlugQuery,
		Result<ProductDetailDTO>>
{
    private readonly CoreDbContext _coreDbContext = coreDbContext;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<ProductDetailDTO>> Handle(GetProductDTOBySlugQuery request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var data = await _coreDbContext.Query<Product>()
                .Select(ProductDetailExtension.ToProductDetailDTO)
                .FirstOrDefaultAsync(x => x.Slug == request.Slug);
            if(data == null)
            {
                return Result.ErrorDataResult<ProductDetailDTO>(Messages.NotFoundData);
            }
            return Result.SuccessDataResult<ProductDetailDTO>(data!);
        },
		cancellationToken);
    }
}