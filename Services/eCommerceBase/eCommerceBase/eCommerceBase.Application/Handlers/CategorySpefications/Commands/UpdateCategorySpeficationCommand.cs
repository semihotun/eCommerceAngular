using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.CategorySpefications.Commands;
public record UpdateCategorySpeficationCommand(Guid? CategoryId,
		Guid? SpecificationAttributeteId,
		System.Guid Id) : IRequest<Result>;
public class UpdateCategorySpeficationCommandHandler(IWriteDbRepository<CategorySpecification> categorySpeficationRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<UpdateCategorySpeficationCommand,
		Result>
{
    private readonly IWriteDbRepository<CategorySpecification> _categorySpeficationRepository = categorySpeficationRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(UpdateCategorySpeficationCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _categorySpeficationRepository.GetAsync(u => u.Id == request.Id);
            if (data is not null)
            {
                data = CategorySpeficationMapper.UpdateCategorySpeficationCommandToCategorySpefication(request);
                _categorySpeficationRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:Category");
                return Result.SuccessResult(Messages.Updated);
            }

            return Result.ErrorResult(Messages.UpdatedError);
        });
    }
}