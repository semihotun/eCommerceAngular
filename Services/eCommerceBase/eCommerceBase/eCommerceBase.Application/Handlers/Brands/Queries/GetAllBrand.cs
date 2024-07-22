using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.Brands.Queries;
public record GetAllBrand() : IRequest<Result<IList<Brand>>>;
public class GetAllBrandHandler(IReadDbRepository<Brand> brandRepository,
		ICacheService cacheService) : IRequestHandler<GetAllBrand,
		Result<IList<Brand>>>
{
    private readonly IReadDbRepository<Brand> _brandRepository = brandRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<IList<Brand>>> Handle(GetAllBrand request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var data = await _brandRepository.ToListAsync();
            return Result.SuccessDataResult(data!);
        },
		cancellationToken);
    }
}