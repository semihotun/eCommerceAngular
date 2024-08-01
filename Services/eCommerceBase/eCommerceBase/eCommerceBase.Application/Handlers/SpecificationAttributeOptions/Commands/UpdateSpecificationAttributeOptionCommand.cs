using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.SpecificationAttributeOptions.Commands;
public record UpdateSpecificationAttributeOptionCommand(Guid SpecificationAttributeId,
		string Name,
		System.Guid Id) : IRequest<Result>;
public class UpdateSpecificationAttributeOptionCommandHandler(IWriteDbRepository<SpecificationAttributeOption> specificationAttributeOptionRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<UpdateSpecificationAttributeOptionCommand,
		Result>
{
    private readonly IWriteDbRepository<SpecificationAttributeOption> _specificationAttributeOptionRepository = specificationAttributeOptionRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(UpdateSpecificationAttributeOptionCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _specificationAttributeOptionRepository.GetAsync(u => u.Id == request.Id);
            if (data is not null)
            {
                data = SpecificationAttributeOptionMapper.UpdateSpecificationAttributeOptionCommandToSpecificationAttributeOption(request);
                _specificationAttributeOptionRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:SpecificationAttribute");
                return Result.SuccessResult(Messages.Updated);
            }

            return Result.ErrorResult(Messages.UpdatedError);
        });
    }
}