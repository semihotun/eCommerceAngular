using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.CategorySpefications.Queries;
public record GetCategorySpeficationByIdQuery(System.Guid Id) : IRequest<Result<CategorySpecification>>;
public class GetCategorySpeficationByIdQueryHandler(IReadDbRepository<CategorySpecification> categorySpeficationRepository,
		ICacheService cacheService) : IRequestHandler<GetCategorySpeficationByIdQuery,
		Result<CategorySpecification>>
{
    private readonly IReadDbRepository<CategorySpecification> _categorySpeficationRepository = categorySpeficationRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<CategorySpecification>> Handle(GetCategorySpeficationByIdQuery request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var query = await _categorySpeficationRepository.GetByIdAsync(request.Id);
            return Result.SuccessDataResult<CategorySpecification>(query!);
        },
		cancellationToken);
    }
}