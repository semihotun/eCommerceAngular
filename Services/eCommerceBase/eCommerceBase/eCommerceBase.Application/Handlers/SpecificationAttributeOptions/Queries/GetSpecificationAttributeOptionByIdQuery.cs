using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.SpecificationAttributeOptions.Queries;
public record GetSpecificationAttributeOptionByIdQuery(System.Guid Id) : IRequest<Result<SpecificationAttributeOption>>;
public class GetSpecificationAttributeOptionByIdQueryHandler(IReadDbRepository<SpecificationAttributeOption> specificationAttributeOptionRepository,
		ICacheService cacheService) : IRequestHandler<GetSpecificationAttributeOptionByIdQuery,
		Result<SpecificationAttributeOption>>
{
    private readonly IReadDbRepository<SpecificationAttributeOption> _specificationAttributeOptionRepository = specificationAttributeOptionRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<SpecificationAttributeOption>> Handle(GetSpecificationAttributeOptionByIdQuery request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var query = await _specificationAttributeOptionRepository.GetByIdAsync(request.Id);
            return Result.SuccessDataResult<SpecificationAttributeOption>(query!);
        },
		cancellationToken);
    }
}