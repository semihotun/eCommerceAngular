using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.UserGroups.Commands;
public record UpdateUserGroupCommand(string Name,
		System.Guid Id) : IRequest<Result>;
public class UpdateUserGroupCommandHandler(IWriteDbRepository<UserGroup> userGroupRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<UpdateUserGroupCommand,
		Result>
{
    private readonly IWriteDbRepository<UserGroup> _userGroupRepository = userGroupRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(UpdateUserGroupCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _userGroupRepository.GetAsync(u => u.Id == request.Id);
            if (data is not null)
            {
                data = UserGroupMapper.UpdateUserGroupCommandToUserGroup(request);
                _userGroupRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:UserGroups");
                return Result.SuccessResult(Messages.Updated);
            }

            return Result.ErrorResult(Messages.UpdatedError);
        });
    }
}