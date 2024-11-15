using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.UserGroupRoles.Commands;
public record UpdateUserGroupRoleCommand(Guid UserGroupId,
		Guid RoleId,
		UserGroup? UserGroup,
		Role? Role,
		System.Guid Id) : IRequest<Result>;
public class UpdateUserGroupRoleCommandHandler(IWriteDbRepository<UserGroupRole> userGroupRoleRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<UpdateUserGroupRoleCommand,
		Result>
{
    private readonly IWriteDbRepository<UserGroupRole> _userGroupRoleRepository = userGroupRoleRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(UpdateUserGroupRoleCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _userGroupRoleRepository.GetAsync(u => u.Id == request.Id);
            if (data is not null)
            {
                data = UserGroupRoleMapper.UpdateUserGroupRoleCommandToUserGroupRole(request);
                _userGroupRoleRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:UserGroupRoles");
                await _cacheService.RemovePatternAsync("eCommerceBase:Roles");
                return Result.SuccessResult(Messages.Updated);
            }

            return Result.ErrorResult(Messages.UpdatedError);
        });
    }
}