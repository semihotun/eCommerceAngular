using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;

namespace eCommerceBase.Application.Handlers.CategorySpefications.Commands;
public record DeleteCategorySpeficationCommand(System.Guid Id) : IRequest<Result>;
public class DeleteCategorySpeficationCommandHandler(IWriteDbRepository<CategorySpecification> categorySpeficationRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<DeleteCategorySpeficationCommand,
		Result>
{
    private readonly IWriteDbRepository<CategorySpecification> _categorySpeficationRepository = categorySpeficationRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(DeleteCategorySpeficationCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _categorySpeficationRepository.GetAsync(p => p.Id == request.Id);
            if (data is not null)
            {
                data.Deleted = true;
                _categorySpeficationRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:Category");
                return Result.SuccessResult(Messages.Deleted);
            }
            else
            {
                return Result.ErrorResult(Messages.DeletedError);
            }
        });
    }
}