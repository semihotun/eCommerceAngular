using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.SpecificationAttributes.Queries;
public record GetSpecificationAttributeByIdQuery(System.Guid Id) : IRequest<Result<SpecificationAttribute>>;
public class GetSpecificationAttributeByIdQueryHandler(IReadDbRepository<SpecificationAttribute> specificationAttributeRepository,
		ICacheService cacheService) : IRequestHandler<GetSpecificationAttributeByIdQuery,
		Result<SpecificationAttribute>>
{
    private readonly IReadDbRepository<SpecificationAttribute> _specificationAttributeRepository = specificationAttributeRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<SpecificationAttribute>> Handle(GetSpecificationAttributeByIdQuery request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var query = await _specificationAttributeRepository.GetByIdAsync(request.Id);
            return Result.SuccessDataResult<SpecificationAttribute>(query!);
        },
		cancellationToken);
    }
}