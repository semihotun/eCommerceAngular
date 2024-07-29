using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.SpecificationAttributes.Commands;
public record UpdateSpecificationAttributeCommand(string Name,
		System.Guid Id) : IRequest<Result>;
public class UpdateSpecificationAttributeCommandHandler(IWriteDbRepository<SpecificationAttribute> specificationAttributeRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<UpdateSpecificationAttributeCommand,
		Result>
{
    private readonly IWriteDbRepository<SpecificationAttribute> _specificationAttributeRepository = specificationAttributeRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(UpdateSpecificationAttributeCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _specificationAttributeRepository.GetAsync(u => u.Id == request.Id);
            if (data is not null)
            {
                data = SpecificationAttributeMapper.UpdateSpecificationAttributeCommandToSpecificationAttribute(request);
                _specificationAttributeRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:SpecificationAttributes");
                return Result.SuccessResult(Messages.Updated);
            }

            return Result.ErrorResult(Messages.UpdatedError);
        });
    }
}