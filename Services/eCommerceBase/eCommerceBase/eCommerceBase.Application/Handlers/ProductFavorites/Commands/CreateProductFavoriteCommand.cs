using eCommerceBase.Application.Constants;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Identity.Middleware;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Persistence.UnitOfWork;
using MediatR;

namespace eCommerceBase.Application.Handlers.ProductFavorites.Commands;
public record CreateProductFavoriteCommand(Guid ProductId) : IRequest<Result<Guid>>;
public class CreateProductFavoriteCommandHandler(IWriteDbRepository<ProductFavorite> productFavoriteRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService,UserScoped userScoped) : IRequestHandler<CreateProductFavoriteCommand,
		Result<Guid>>
{
    private readonly IWriteDbRepository<ProductFavorite> _productFavoriteRepository = productFavoriteRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    private readonly UserScoped _userScoped = userScoped;

    /// <summary>
    /// User Id'e göre cache silinmeli
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Result<Guid>> Handle(CreateProductFavoriteCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = new ProductFavorite(request.ProductId, _userScoped.Id);
            await _productFavoriteRepository.AddAsync(data);
            await _cacheService.RemovePatternAsync("eCommerceBase:Product");
            return Result.SuccessDataResult(data.Id,Messages.Added);
        });
    }
}