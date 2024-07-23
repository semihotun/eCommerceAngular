using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.SpecificationAttributeOptions.Commands;
public record CreateSpecificationAttributeOptionCommand(Guid SpecificationAttributeId,
		string Name,
		int DisplayOrder,
		SpecificationAttribute? SpecificationAttribute) : IRequest<Result>;
public class CreateSpecificationAttributeOptionCommandHandler(IWriteDbRepository<SpecificationAttributeOption> specificationAttributeOptionRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<CreateSpecificationAttributeOptionCommand,
		Result>
{
    private readonly IWriteDbRepository<SpecificationAttributeOption> _specificationAttributeOptionRepository = specificationAttributeOptionRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(CreateSpecificationAttributeOptionCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = SpecificationAttributeOptionMapper.CreateSpecificationAttributeOptionCommandToSpecificationAttributeOption(request);
            data.SetSpecificationAttribute(request.SpecificationAttribute);
            await _specificationAttributeOptionRepository.AddAsync(data);
            await _cacheService.RemovePatternAsync("eCommerceBase:SpecificationAttributeOptions");
            return Result.SuccessResult(Messages.Added);
        });
    }
}