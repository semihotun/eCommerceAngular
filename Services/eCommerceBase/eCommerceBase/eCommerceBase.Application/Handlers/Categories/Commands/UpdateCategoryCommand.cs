using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.Categories.Commands;
public record UpdateCategoryCommand(string CategoryName,
		Guid? ParentCategoryId,
		System.Guid Id) : IRequest<Result>;
public class UpdateCategoryCommandHandler(IWriteDbRepository<Category> categoryRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<UpdateCategoryCommand,
		Result>
{
    private readonly IWriteDbRepository<Category> _categoryRepository = categoryRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(UpdateCategoryCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _categoryRepository.GetAsync(u => u.Id == request.Id);
            if (data is not null)
            {
                data = CategoryMapper.UpdateCategoryCommandToCategory(request);
                _categoryRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:Categories");
                return Result.SuccessResult(Messages.Updated);
            }

            return Result.ErrorResult(Messages.UpdatedError);
        });
    }
}