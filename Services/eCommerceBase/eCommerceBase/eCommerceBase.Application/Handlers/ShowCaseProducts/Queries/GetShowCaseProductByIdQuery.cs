using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.ShowCaseProducts.Queries;
public record GetShowCaseProductByIdQuery(System.Guid Id) : IRequest<Result<ShowCaseProduct>>;
public class GetShowCaseProductByIdQueryHandler(IReadDbRepository<ShowCaseProduct> showCaseProductRepository,
		ICacheService cacheService) : IRequestHandler<GetShowCaseProductByIdQuery,
		Result<ShowCaseProduct>>
{
    private readonly IReadDbRepository<ShowCaseProduct> _showCaseProductRepository = showCaseProductRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<ShowCaseProduct>> Handle(GetShowCaseProductByIdQuery request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var query = await _showCaseProductRepository.GetByIdAsync(request.Id);
            return Result.SuccessDataResult<ShowCaseProduct>(query!);
        },
		cancellationToken);
    }
}