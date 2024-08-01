using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.Categories.Commands;
public record CreateCategoryCommand(string CategoryName,
		Guid? ParentCategoryId) : IRequest<Result>;
public class CreateCategoryCommandHandler(IWriteDbRepository<Category> categoryRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<CreateCategoryCommand,
		Result>
{
    private readonly IWriteDbRepository<Category> _categoryRepository = categoryRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(CreateCategoryCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = CategoryMapper.CreateCategoryCommandToCategory(request);
            await _categoryRepository.AddAsync(data);
            await _cacheService.RemovePatternAsync("eCommerceBase:Categories");
            return Result.SuccessResult(Messages.Added);
        });
    }
}