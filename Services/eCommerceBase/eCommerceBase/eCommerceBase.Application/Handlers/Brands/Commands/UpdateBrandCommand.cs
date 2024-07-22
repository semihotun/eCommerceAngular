using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.Brands.Commands;
public record UpdateBrandCommand(string BrandName,
		System.Guid Id) : IRequest<Result>;
public class UpdateBrandCommandHandler(IWriteDbRepository<Brand> brandRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<UpdateBrandCommand,
		Result>
{
    private readonly IWriteDbRepository<Brand> _brandRepository = brandRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(UpdateBrandCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _brandRepository.GetAsync(u => u.Id == request.Id);
            if (data is not null)
            {
                BrandMapper.UpdateBrandCommandToBrand(request,
		data);
                _brandRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:Brands");
                return Result.SuccessResult(Messages.Updated);
            }

            return Result.ErrorResult(Messages.UpdatedError);
        });
    }
}