using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.Categories.Queries;
public record GetCategoryByIdQuery(System.Guid Id) : IRequest<Result<Category>>;
public class GetCategoryByIdQueryHandler(IReadDbRepository<Category> categoryRepository,
		ICacheService cacheService) : IRequestHandler<GetCategoryByIdQuery,
		Result<Category>>
{
    private readonly IReadDbRepository<Category> _categoryRepository = categoryRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<Category>> Handle(GetCategoryByIdQuery request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var query = await _categoryRepository.GetByIdAsync(request.Id);
            return Result.SuccessDataResult<Category>(query!);
        },
		cancellationToken);
    }
}