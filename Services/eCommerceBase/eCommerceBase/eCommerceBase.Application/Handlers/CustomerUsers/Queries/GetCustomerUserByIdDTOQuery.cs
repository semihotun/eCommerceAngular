using eCommerceBase.Application.Handlers.CustomerUsers.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Identity.Middleware;
using eCommerceBase.Persistence.GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerceBase.Application.Handlers.CustomerUsers.Queries;
public record GetCustomerUserByIdDTOQuery() : IRequest<Result<CustomerUserDTO>>;
public class GetCustomerUserByIdQueryHandler(IReadDbRepository<CustomerUser> customerUserRepository,
        ICacheService cacheService, UserScoped userScoped) : IRequestHandler<GetCustomerUserByIdDTOQuery,
        Result<CustomerUserDTO>>
{
    private readonly IReadDbRepository<CustomerUser> _customerUserRepository = customerUserRepository;
    private readonly ICacheService _cacheService = cacheService;
    private readonly UserScoped _userScoped = userScoped;

    public async Task<Result<CustomerUserDTO>> Handle(GetCustomerUserByIdDTOQuery request,
        CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request, _userScoped,
        async () =>
        {
            var result =await _customerUserRepository.Query()
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