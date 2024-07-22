using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.Brands.Commands;
public record CreateBrandCommand(string BrandName) : IRequest<Result>;
public class CreateBrandCommandHandler(IWriteDbRepository<Brand> brandRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<CreateBrandCommand,
		Result>
{
    private readonly IWriteDbRepository<Brand> _brandRepository = brandRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(CreateBrandCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = BrandMapper.CreateBrandCommandToBrand(request);
            await _brandRepository.AddAsync(data);
            await _cacheService.RemovePatternAsync("eCommerceBase:Brands");
            return Result.SuccessResult(Messages.Added);
        });
    }
}