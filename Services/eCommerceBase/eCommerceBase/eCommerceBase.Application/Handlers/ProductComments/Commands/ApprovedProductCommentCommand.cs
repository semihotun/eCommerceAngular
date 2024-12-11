using eCommerceBase.Application.Constants;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Identity.Middleware;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Persistence.UnitOfWork;
using MediatR;

namespace eCommerceBase.Application.Handlers.ProductComments.Commands;
public record ApprovedProductCommentCommand(Guid Id) : IRequest<Result>;
public class ApprovedProductCommentCommandHandler(IWriteDbRepository<ProductComment> productCommentRepository,
        IUnitOfWork unitOfWork,
        ICacheService cacheService,
        UserScoped userScoped) : IRequestHandler<ApprovedProductCommentCommand,
        Result>
{
    private readonly IWriteDbRepository<ProductComment> _productCommentRepository = productCommentRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    private readonly UserScoped _userScoped = userScoped;

    public async Task<Result> Handle(ApprovedProductCommentCommand request,
        CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _productCommentRepository.GetByIdAsync(request.Id);
            if (data != null)
            {
                data.ApprovedComment();
                await _productCommentRepository.AddAsync(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:ProductComment");
                return Result.SuccessResult(Messages.OperationSuccess);
            }
            else
            {
                return Result.SuccessResult(Messages.OperationError);
            }
        });
    }
}
