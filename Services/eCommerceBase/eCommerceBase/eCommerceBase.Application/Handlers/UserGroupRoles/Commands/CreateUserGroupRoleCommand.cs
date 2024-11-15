using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.UserGroupRoles.Commands;
public record CreateUserGroupRoleCommand(Guid UserGroupId,
		Guid RoleId,
		UserGroup? UserGroup,
		Role? Role) : IRequest<Result>;
public class CreateUserGroupRoleCommandHandler(IWriteDbRepository<UserGroupRole> userGroupRoleRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<CreateUserGroupRoleCommand,
		Result>
{
    private readonly IWriteDbRepository<UserGroupRole> _userGroupRoleRepository = userGroupRoleRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(CreateUserGroupRoleCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = UserGroupRoleMapper.CreateUserGroupRoleCommandToUserGroupRole(request);
            await _userGroupRoleRepository.AddAsync(data);
            await _cacheService.RemovePatternAsync("eCommerceBase:UserGroupRoles");
            await _cacheService.RemovePatternAsync("eCommerceBase:Roles");
            return Result.SuccessResult(Messages.Added);
        });
    }
}