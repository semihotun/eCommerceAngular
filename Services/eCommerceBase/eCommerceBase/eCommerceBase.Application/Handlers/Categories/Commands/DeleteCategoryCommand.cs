using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;

namespace eCommerceBase.Application.Handlers.Categories.Commands;
public record DeleteCategoryCommand(System.Guid Id) : IRequest<Result>;
public class DeleteCategoryCommandHandler(IWriteDbRepository<Category> categoryRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<DeleteCategoryCommand,
		Result>
{
    private readonly IWriteDbRepository<Category> _categoryRepository = categoryRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(DeleteCategoryCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _categoryRepository.GetAsync(p => p.Id == request.Id);
            if (data is not null)
            {
                await DeleteCategoryAndSubCategories(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:Categories");
                return Result.SuccessResult(Messages.Deleted);
            }
            else
            {
                return Result.ErrorResult(Messages.DeletedError);
            }
        });
    }
    private async Task DeleteCategoryAndSubCategories(Category category)
    {
        var subCategories = await _categoryRepository.ToListAsync(c => c.ParentCategoryId == category.Id);
        category.Deleted = true;
        _categoryRepository.Update(category);
        foreach (var subCategory in subCategories)
        {
            await DeleteCategoryAndSubCategories(subCategory);
        }
    }
}