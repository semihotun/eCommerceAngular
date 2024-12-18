using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.CategorySpecifications.Commands;
public record UpdateCategorySpecificationCommand(Guid? CategoryId,
		Guid? SpecificationAttributeteId,
		System.Guid Id) : IRequest<Result>;
public class UpdateCategorySpecificationCommandHandler(IWriteDbRepository<CategorySpecification> categorySpecificationRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<UpdateCategorySpecificationCommand,
		Result>
{
    private readonly IWriteDbRepository<CategorySpecification> _categorySpecificationRepository = categorySpecificationRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(UpdateCategorySpecificationCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _categorySpecificationRepository.GetAsync(u => u.Id == request.Id);
            if (data is not null)
            {
                data = CategorySpecificationMapper.UpdateCategorySpecificationCommandToCategorySpecification(request);
                _categorySpecificationRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:CategorySpecifications");
                return Result.SuccessResult(Messages.Updated);
            }

            return Result.ErrorResult(Messages.UpdatedError);
        });
    }
}