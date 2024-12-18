using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using Microsoft.EntityFrameworkCore;

namespace eCommerceBase.Application.Handlers.ProductShipmentInfoes.Queries;
public record GetProductShipmentInfoByProductIdQuery(System.Guid ProductId) : IRequest<Result<ProductShipmentInfo>>;
public class GetProductShipmentInfoByProductIdQueryHandler(IReadDbRepository<ProductShipmentInfo> productShipmentInfoRepository,
		ICacheService cacheService) : IRequestHandler<GetProductShipmentInfoByProductIdQuery,
		Result<ProductShipmentInfo>>
{
    private readonly IReadDbRepository<ProductShipmentInfo> _productShipmentInfoRepository = productShipmentInfoRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<ProductShipmentInfo>> Handle(GetProductShipmentInfoByProductIdQuery request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var query = await _productShipmentInfoRepository.Query().Where(x=>x.ProductId == request.ProductId).FirstOrDefaultAsync();
            return Result.SuccessDataResult(query!);
        },
		cancellationToken);
    }
}