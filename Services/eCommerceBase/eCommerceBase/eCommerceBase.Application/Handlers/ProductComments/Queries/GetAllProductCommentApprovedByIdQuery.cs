using eCommerceBase.Application.Handlers.ProductComments.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Persistence.GenericRepository;
using MediatR;

namespace eCommerceBase.Application.Handlers.ProductComments.Queries;
public record GetAllProductCommentApprovedByIdQuery(Guid ProductId,int PageIndex, int PageSize)
    : IRequest<Result<PagedList<GetAllProductCommentApprovedByProductIdDto>>>;
public class GetGetAllProductCommentApprovedQueryHandler(IReadDbRepository<ProductComment> productCommentRepository, ICacheService cacheService)
    : IRequestHandler<GetAllProductCommentApprovedByIdQuery, Result<PagedList<GetAllProductCommentApprovedByProductIdDto>>>
{
    private readonly IReadDbRepository<ProductComment> _productCommentRepository = productCommentRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<GetAllProductCommentApprovedByProductIdDto>>> Handle(GetAllProductCommentApprovedByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request, async () =>
        {
            var query = await _productCommentRepository.Query()
            .Where(x=>x.ProductId==request.ProductId && x.IsApprove)
            .Select(x => new GetAllProductCommentApprovedByProductIdDto
            {
                Id = x.Id,
                IsApprove = x.IsApprove,
                CustomerUserFirstName = x.CustomerUser!.FirstName,
                CustomerUserLastName = x.CustomerUser!.LastName,
                Comment = x.Comment,
                CreatedDate=x.CreatedOnUtc
            }).ToPagedListAsync(request.PageIndex, request.PageSize);
            return Result.SuccessDataResult(query);
        }, cancellationToken);
    }
}