using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using System.Diagnostics;

namespace eCommerceBase.Application.Handlers.Roles.Queries;
public record GetAllRole() : IRequest<Result<IList<Role>>>;
public class GetAllRoleHandler(IReadDbRepository<Role> roleRepository,
		ICacheService cacheService) : IRequestHandler<GetAllRole,
		Result<IList<Role>>>
{
    private readonly IReadDbRepository<Role> _roleRepository = roleRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<IList<Role>>> Handle(GetAllRole request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var data = await _roleRepository.ToListAsync();
            return Result.SuccessDataResult(data!);
        },
		cancellationToken);
    }
}