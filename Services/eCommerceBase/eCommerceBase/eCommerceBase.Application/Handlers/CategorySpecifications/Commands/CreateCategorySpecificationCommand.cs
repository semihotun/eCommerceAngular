using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.CategorySpecifications.Commands;
public record CreateCategorySpecificationCommand(Guid? CategoryId,
		Guid? SpecificationAttributeteId) : IRequest<Result>;
public class CreateCategorySpecificationCommandHandler(IWriteDbRepository<CategorySpecification> categorySpecificationRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<CreateCategorySpecificationCommand,
		Result>
{
    private readonly IWriteDbRepository<CategorySpecification> _categorySpecificationRepository = categorySpecificationRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(CreateCategorySpecificationCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = CategorySpecificationMapper.CreateCategorySpecificationCommandToCategorySpecification(request);
            await _categorySpecificationRepository.AddAsync(data);
            await _cacheService.RemovePatternAsync("eCommerceBase:CategorySpecifications");
            return Result.SuccessResult(Messages.Added);
        });
    }
}