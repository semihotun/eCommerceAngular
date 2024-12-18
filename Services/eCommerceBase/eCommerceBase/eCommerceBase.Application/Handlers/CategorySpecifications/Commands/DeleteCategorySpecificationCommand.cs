using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;

namespace eCommerceBase.Application.Handlers.CategorySpecifications.Commands;
public record DeleteCategorySpecificationCommand(System.Guid Id) : IRequest<Result>;
public class DeleteCategorySpecificationCommandHandler(IWriteDbRepository<CategorySpecification> categorySpecificationRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<DeleteCategorySpecificationCommand,
		Result>
{
    private readonly IWriteDbRepository<CategorySpecification> _categorySpecificationRepository = categorySpecificationRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(DeleteCategorySpecificationCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _categorySpecificationRepository.GetAsync(p => p.Id == request.Id);
            if (data is not null)
            {
                data.Deleted = true;
                _categorySpecificationRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:CategorySpecifications");
                return Result.SuccessResult(Messages.Deleted);
            }
            else
            {
                return Result.ErrorResult(Messages.DeletedError);
            }
        });
    }
}