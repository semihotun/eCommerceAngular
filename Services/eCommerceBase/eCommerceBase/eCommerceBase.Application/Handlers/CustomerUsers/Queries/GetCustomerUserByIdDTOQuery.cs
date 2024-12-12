using eCommerceBase.Application.Handlers.CustomerUsers.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Identity.Middleware;
using eCommerceBase.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerceBase.Application.Handlers.CustomerUsers.Queries;
public record GetCustomerUserByIdDTOQuery() : IRequest<Result<CustomerUserDTO>>;
public class GetCustomerUserByIdQueryHandler(ICoreDbContext coreDbContext,
        ICacheService cacheService, UserScoped userScoped) : IRequestHandler<GetCustomerUserByIdDTOQuery,
        Result<CustomerUserDTO>>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly ICacheService _cacheService = cacheService;
    private readonly UserScoped _userScoped = userScoped;

    public async Task<Result<CustomerUserDTO>> Handle(GetCustomerUserByIdDTOQuery request,
        CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request, _userScoped,
        async () =>
        {
            var result =await _coreDbContext.Query<CustomerUser>()
            .Where(x => x.Id == _userScoped.Id)
            .Select(x => new CustomerUserDTO
            {
                CreatedOnUtc = x.CreatedOnUtc,
                FirstName=x.FirstName,
                LastName=x.LastName,
                Email=x.Email,
                IsActivationApprove=x.IsActivationApprove,
            }).FirstOrDefaultAsync();

            return Result.SuccessDataResult(result!);
        },
        cancellationToken);
    }
}