using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Insfrastructure.Utilities.Identity.Middleware;

namespace eCommerceBase.Application.Handlers.ProductComments.Commands;
public record CreateProductCommentCommand(Guid ProductId,
		string? Comment , int Rate) : IRequest<Result>;
public class CreateProductCommentCommandHandler(IWriteDbRepository<ProductComment> productCommentRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService,
        UserScoped userScoped) : IRequestHandler<CreateProductCommentCommand,
		Result>
{
    private readonly IWriteDbRepository<ProductComment> _productCommentRepository = productCommentRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    private readonly UserScoped _userScoped = userScoped;

    public async Task<Result> Handle(CreateProductCommentCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = new ProductComment(request.ProductId,request.Comment,request.Rate,false);
            data.SetCustomerUserId(_userScoped.Id);
            await _productCommentRepository.AddAsync(data);
            await _cacheService.RemovePatternAsync("eCommerceBase:ProductComment");
            return Result.SuccessResult(Messages.Added);
        });
    }
}