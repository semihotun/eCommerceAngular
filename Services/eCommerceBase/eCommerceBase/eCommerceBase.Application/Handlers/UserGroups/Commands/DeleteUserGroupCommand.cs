using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;

namespace eCommerceBase.Application.Handlers.UserGroups.Commands;
public record DeleteUserGroupCommand(System.Guid Id) : IRequest<Result>;
public class DeleteUserGroupCommandHandler(IWriteDbRepository<UserGroup> userGroupRepository,
    IWriteDbRepository<UserGroupRole> userGroupRoleRepository,
        IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<DeleteUserGroupCommand,
		Result>
{
    private readonly IWriteDbRepository<UserGroup> _userGroupRepository = userGroupRepository;
    private readonly IWriteDbRepository<UserGroupRole> _userGroupRoleRepository = userGroupRoleRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(DeleteUserGroupCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _userGroupRepository.GetAsync(p => p.Id == request.Id && p.IsEditable);
            if (data is not null)
            {
                data.Deleted = true;
                _userGroupRepository.Update(data);
                var userGroupRoleList =await _userGroupRoleRepository.ToListAsync(x => x.UserGroupId == data.Id);
                foreach (var userGroupRole in userGroupRoleList)
                {
                    userGroupRole.Deleted = true;
                    _userGroupRoleRepository.Update(userGroupRole);
                }
                await _cacheService.RemovePatternAsync("eCommerceBase:UserGroups");
                return Result.SuccessResult(Messages.Deleted);
            }
            else
            {
                return Result.ErrorResult(Messages.DeletedError);
            }
        });
    }
}