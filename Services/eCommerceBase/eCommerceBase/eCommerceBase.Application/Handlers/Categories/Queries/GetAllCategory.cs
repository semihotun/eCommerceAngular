using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Handlers.Categories.Queries.Dtos;

namespace eCommerceBase.Application.Handlers.Categories.Queries;
public record GetAllCategory() : IRequest<Result<List<CategoryTreeDTO>>>;
public class GetAllCategoryHandler(IReadDbRepository<Category> categoryRepository,
		ICacheService cacheService) : IRequestHandler<GetAllCategory,
		Result<List<CategoryTreeDTO>>>
{
    private readonly IReadDbRepository<Category> _categoryRepository = categoryRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<List<CategoryTreeDTO>>> Handle(GetAllCategory request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var categories = await _categoryRepository.ToListAsync();
            var categoryLookup = categories.ToLookup(c => c.ParentCategoryId);
            List<CategoryTreeDTO> BuildCategoryTree(Guid? parentId)
            {
                return categoryLookup[parentId]
                    .Select(c => new CategoryTreeDTO
                    {
                        Id=c.Id,
                        CategoryName = c.CategoryName,
                        Slug= c.Slug,   
                        SubCategories = BuildCategoryTree(c.Id)
                    }).ToList();
            }
            return Result.SuccessDataResult(BuildCategoryTree(null)!);
        },
		cancellationToken);
    }
}
