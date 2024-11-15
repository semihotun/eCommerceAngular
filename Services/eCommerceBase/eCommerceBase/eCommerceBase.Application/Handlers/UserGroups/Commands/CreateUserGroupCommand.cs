using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.UserGroups.Commands;
public record CreateUserGroupCommand(string Name) : IRequest<Result>;
public class CreateUserGroupCommandHandler(IWriteDbRepository<UserGroup> userGroupRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<CreateUserGroupCommand,
		Result>
{
    private readonly IWriteDbRepository<UserGroup> _userGroupRepository = userGroupRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(CreateUserGroupCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = UserGroupMapper.CreateUserGroupCommandToUserGroup(request);
            await _userGroupRepository.AddAsync(data);
            await _cacheService.RemovePatternAsync("eCommerceBase:UserGroups");
            return Result.SuccessResult(Messages.Added);
        });
    }
}