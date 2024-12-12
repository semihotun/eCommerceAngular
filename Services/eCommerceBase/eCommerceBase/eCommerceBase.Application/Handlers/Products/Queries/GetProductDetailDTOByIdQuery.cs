using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Products.Extenison;
using eCommerceBase.Application.Handlers.Products.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Identity.Middleware;
using eCommerceBase.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerceBase.Application.Handlers.Products.Queries
{
    public record GetProductDetailDTOByIdQuery(System.Guid? Id) : IRequest<Result<ProductDetailDTO>>;

    public class GetProductDetailDTOByIdQueryHandler : IRequestHandler<GetProductDetailDTOByIdQuery, Result<ProductDetailDTO>>
    {
        private readonly CoreDbContext _coreDbContext;
        private readonly ICacheService _cacheService;
        private readonly UserScoped _userScoped;
        public GetProductDetailDTOByIdQueryHandler(CoreDbContext coreDbContext, ICacheService cacheService, UserScoped userScoped)
        {
            _coreDbContext = coreDbContext;
            _cacheService = cacheService;
            _userScoped = userScoped;
        }

        public async Task<Result<ProductDetailDTO>> Handle(GetProductDetailDTOByIdQuery request, CancellationToken cancellationToken)
        {
            return await _cacheService.GetAsync(request, _userScoped, async () =>
            {
                var data = await _coreDbContext.Query<Product>()
                        .Select(ProductDetailExtension.ToProductDetailDTO(_userScoped.Id))
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                if (data == null)
                {
                    return Result.ErrorDataResult<ProductDetailDTO>(Messages.NotFoundData);
                }
                return Result.SuccessDataResult<ProductDetailDTO>(data!);
            }, cancellationToken);
        }
    }
}
