using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.CategorySpefications.Commands;
public record CreateCategorySpeficationCommand(Guid? CategoryId,
		Guid? SpecificationAttributeteId) : IRequest<Result>;
public class CreateCategorySpeficationCommandHandler(IWriteDbRepository<CategorySpecification> categorySpeficationRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<CreateCategorySpeficationCommand,
		Result>
{
    private readonly IWriteDbRepository<CategorySpecification> _categorySpeficationRepository = categorySpeficationRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(CreateCategorySpeficationCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = CategorySpeficationMapper.CreateCategorySpeficationCommandToCategorySpefication(request);
            await _categorySpeficationRepository.AddAsync(data);
            await _cacheService.RemovePatternAsync("eCommerceBase:Category");
            return Result.SuccessResult(Messages.Added);
        });
    }
}