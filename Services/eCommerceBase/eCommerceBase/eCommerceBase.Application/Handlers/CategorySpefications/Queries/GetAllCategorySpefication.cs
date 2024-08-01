using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.CategorySpefications.Queries;
public record GetAllCategorySpefication() : IRequest<Result<IList<CategorySpecification>>>;
public class GetAllCategorySpeficationHandler(IReadDbRepository<CategorySpecification> categorySpeficationRepository,
		ICacheService cacheService) : IRequestHandler<GetAllCategorySpefication,
		Result<IList<CategorySpecification>>>
{
    private readonly IReadDbRepository<CategorySpecification> _categorySpeficationRepository = categorySpeficationRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<IList<CategorySpecification>>> Handle(GetAllCategorySpefication request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var data = await _categorySpeficationRepository.ToListAsync();
            return Result.SuccessDataResult(data!);
        },
		cancellationToken);
    }
}