using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.CategorySpecifications.Queries;
public record GetCategorySpecificationByIdQuery(System.Guid Id) : IRequest<Result<CategorySpecification>>;
public class GetCategorySpecificationByIdQueryHandler(IReadDbRepository<CategorySpecification> categorySpecificationRepository,
		ICacheService cacheService) : IRequestHandler<GetCategorySpecificationByIdQuery,
		Result<CategorySpecification>>
{
    private readonly IReadDbRepository<CategorySpecification> _categorySpecificationRepository = categorySpecificationRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<CategorySpecification>> Handle(GetCategorySpecificationByIdQuery request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var query = await _categorySpecificationRepository.GetByIdAsync(request.Id);
            return Result.SuccessDataResult<CategorySpecification>(query!);
        },
		cancellationToken);
    }
}