using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.CategorySpecifications.Queries;
public record GetAllCategorySpecification() : IRequest<Result<IList<CategorySpecification>>>;
public class GetAllCategorySpecificationHandler(IReadDbRepository<CategorySpecification> categorySpecificationRepository,
		ICacheService cacheService) : IRequestHandler<GetAllCategorySpecification,
		Result<IList<CategorySpecification>>>
{
    private readonly IReadDbRepository<CategorySpecification> _categorySpecificationRepository = categorySpecificationRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<IList<CategorySpecification>>> Handle(GetAllCategorySpecification request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var data = await _categorySpecificationRepository.ToListAsync();
            return Result.SuccessDataResult(data!);
        },
		cancellationToken);
    }
}