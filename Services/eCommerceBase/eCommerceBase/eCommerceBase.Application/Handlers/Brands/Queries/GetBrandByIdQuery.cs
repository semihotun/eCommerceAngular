using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.Brands.Queries;
public record GetBrandByIdQuery(System.Guid Id) : IRequest<Result<Brand>>;
public class GetBrandByIdQueryHandler(IReadDbRepository<Brand> brandRepository,
		ICacheService cacheService) : IRequestHandler<GetBrandByIdQuery,
		Result<Brand>>
{
    private readonly IReadDbRepository<Brand> _brandRepository = brandRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<Brand>> Handle(GetBrandByIdQuery request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var query = await _brandRepository.GetByIdAsync(request.Id);
            return Result.SuccessDataResult<Brand>(query!);
        },
		cancellationToken);
    }
}