using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.SpecificationAttributeOptions.Queries;
public record GetAllSpecificationAttributeOption() : IRequest<Result<IList<SpecificationAttributeOption>>>;
public class GetAllSpecificationAttributeOptionHandler(IReadDbRepository<SpecificationAttributeOption> specificationAttributeOptionRepository,
		ICacheService cacheService) : IRequestHandler<GetAllSpecificationAttributeOption,
		Result<IList<SpecificationAttributeOption>>>
{
    private readonly IReadDbRepository<SpecificationAttributeOption> _specificationAttributeOptionRepository = specificationAttributeOptionRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<IList<SpecificationAttributeOption>>> Handle(GetAllSpecificationAttributeOption request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var data = await _specificationAttributeOptionRepository.ToListAsync();
            return Result.SuccessDataResult(data!);
        },
		cancellationToken);
    }
}