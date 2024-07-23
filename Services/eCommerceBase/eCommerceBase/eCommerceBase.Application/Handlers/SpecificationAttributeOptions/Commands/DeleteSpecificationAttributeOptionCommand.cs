using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;

namespace eCommerceBase.Application.Handlers.SpecificationAttributeOptions.Commands;
public record DeleteSpecificationAttributeOptionCommand(System.Guid Id) : IRequest<Result>;
public class DeleteSpecificationAttributeOptionCommandHandler(IWriteDbRepository<SpecificationAttributeOption> specificationAttributeOptionRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<DeleteSpecificationAttributeOptionCommand,
		Result>
{
    private readonly IWriteDbRepository<SpecificationAttributeOption> _specificationAttributeOptionRepository = specificationAttributeOptionRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(DeleteSpecificationAttributeOptionCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _specificationAttributeOptionRepository.GetAsync(p => p.Id == request.Id);
            if (data is not null)
            {
                data.Deleted = true;
                _specificationAttributeOptionRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:SpecificationAttributeOptions");
                return Result.SuccessResult(Messages.Deleted);
            }
            else
            {
                return Result.ErrorResult(Messages.DeletedError);
            }
        });
    }
}