using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;

namespace eCommerceBase.Application.Handlers.UserGroupRoles.Commands;
public record DeleteUserGroupRoleCommand(System.Guid Id) : IRequest<Result>;
public class DeleteUserGroupRoleCommandHandler(IWriteDbRepository<UserGroupRole> userGroupRoleRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<DeleteUserGroupRoleCommand,
		Result>
{
    private readonly IWriteDbRepository<UserGroupRole> _userGroupRoleRepository = userGroupRoleRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(DeleteUserGroupRoleCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _userGroupRoleRepository.GetAsync(p => p.Id == request.Id);
            if (data is not null)
            {
                data.Deleted = true;
                _userGroupRoleRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:UserGroupRoles");
                await _cacheService.RemovePatternAsync("eCommerceBase:Roles");
                return Result.SuccessResult(Messages.Deleted);
            }
            else
            {
                return Result.ErrorResult(Messages.DeletedError);
            }
        });
    }
}