using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.SpecificationAttributes.Commands;
public record CreateSpecificationAttributeCommand(string Name) : IRequest<Result>;
public class CreateSpecificationAttributeCommandHandler(IWriteDbRepository<SpecificationAttribute> specificationAttributeRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<CreateSpecificationAttributeCommand,
		Result>
{
    private readonly IWriteDbRepository<SpecificationAttribute> _specificationAttributeRepository = specificationAttributeRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(CreateSpecificationAttributeCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = SpecificationAttributeMapper.CreateSpecificationAttributeCommandToSpecificationAttribute(request);
            await _specificationAttributeRepository.AddAsync(data);
            await _cacheService.RemovePatternAsync("eCommerceBase:SpecificationAttributes");
            return Result.SuccessResult(Messages.Added);
        });
    }
}