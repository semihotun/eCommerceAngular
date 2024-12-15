using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using Microsoft.EntityFrameworkCore;

namespace eCommerceBase.Application.Handlers.SpecificationAttributeOptions.Queries;
public record GetAllSpecificationAttributeOptionBySpecificationAttributeId(Guid SpecificationAttributeId) 
    : IRequest<Result<List<SpecificationAttributeOption>>>;
public class GetAllSpecificationAttributeBySpecificationAttributeIdHandler(IReadDbRepository<SpecificationAttributeOption> specificationAttributeOptionRepository,
		ICacheService cacheService) : IRequestHandler<GetAllSpecificationAttributeOptionBySpecificationAttributeId,
		Result<List<SpecificationAttributeOption>>>
{
    private readonly IReadDbRepository<SpecificationAttributeOption> _specificationAttributeOptionRepository = specificationAttributeOptionRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<List<SpecificationAttributeOption>>> Handle(GetAllSpecificationAttributeOptionBySpecificationAttributeId request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var data = await _specificationAttributeOptionRepository.Query().Where(x=>x.SpecificationAttributeId == request.SpecificationAttributeId).ToListAsync();
            return Result.SuccessDataResult(data!);
        },
		cancellationToken);
    }
}