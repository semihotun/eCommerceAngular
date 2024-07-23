using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.SpecificationAttributes.Queries;
public record GetAllSpecificationAttribute() : IRequest<Result<IList<SpecificationAttribute>>>;
public class GetAllSpecificationAttributeHandler(IReadDbRepository<SpecificationAttribute> specificationAttributeRepository,
		ICacheService cacheService) : IRequestHandler<GetAllSpecificationAttribute,
		Result<IList<SpecificationAttribute>>>
{
    private readonly IReadDbRepository<SpecificationAttribute> _specificationAttributeRepository = specificationAttributeRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<IList<SpecificationAttribute>>> Handle(GetAllSpecificationAttribute request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var data = await _specificationAttributeRepository.ToListAsync();
            return Result.SuccessDataResult(data!);
        },
		cancellationToken);
    }
}