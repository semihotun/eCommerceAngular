using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Identity.Middleware;
using eCommerceBase.Application.Constants;

namespace eCommerceBase.Application.Handlers.ProductComments.Commands;
public record DisapprovedProductCommentCommand(Guid Id) : IRequest<Result>;
public class DisapprovedProductCommentCommandHandler(IWriteDbRepository<ProductComment> productCommentRepository,
        IUnitOfWork unitOfWork,
        ICacheService cacheService,
        UserScoped userScoped) : IRequestHandler<DisapprovedProductCommentCommand,
        Result>
{
    private readonly IWriteDbRepository<ProductComment> _productCommentRepository = productCommentRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    private readonly UserScoped _userScoped = userScoped;

    public async Task<Result> Handle(DisapprovedProductCommentCommand request,
        CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data =await _productCommentRepository.GetByIdAsync(request.Id);
            if(data != null)
            {
                data.DısApprovedComment();
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
