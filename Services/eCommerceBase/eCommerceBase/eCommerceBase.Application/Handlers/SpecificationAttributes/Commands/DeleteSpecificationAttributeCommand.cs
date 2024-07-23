using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;

namespace eCommerceBase.Application.Handlers.SpecificationAttributes.Commands;
public record DeleteSpecificationAttributeCommand(System.Guid Id) : IRequest<Result>;
public class DeleteSpecificationAttributeCommandHandler(IWriteDbRepository<SpecificationAttribute> specificationAttributeRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<DeleteSpecificationAttributeCommand,
		Result>
{
    private readonly IWriteDbRepository<SpecificationAttribute> _specificationAttributeRepository = specificationAttributeRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(DeleteSpecificationAttributeCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _specificationAttributeRepository.GetAsync(p => p.Id == request.Id);
            if (data is not null)
            {
                data.Deleted = true;
                _specificationAttributeRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:SpecificationAttributes");
                return Result.SuccessResult(Messages.Deleted);
            }
            else
            {
                return Result.ErrorResult(Messages.DeletedError);
            }
        });
    }
}