using eCommerceBase.Application.Handlers.UserGroupRoles.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Persistence.GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerceBase.Application.Handlers.UserGroupRoles.Queries;
public record GetUserGroupRoleGridDTOQuery(Guid UserGroupId, int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList) 
    : IRequest<Result<PagedList<UserGroupRoleGridDTO>>>;
public class GetUserGroupRoleGridDTOQueryHandler(IReadDbRepository<UserGroupRole> userGroupRoleRepository, ICacheService cacheService) 
    : IRequestHandler<GetUserGroupRoleGridDTOQuery, Result<PagedList<UserGroupRoleGridDTO>>>
{
    private readonly IReadDbRepository<UserGroupRole> _userGroupRoleRepository = userGroupRoleRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<UserGroupRoleGridDTO>>> Handle(GetUserGroupRoleGridDTOQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request, async () =>
        {
            var query = await _userGroupRoleRepository.Query()
            .Include(x => x.Role)
            .Where(x => x.UserGroupId == request.UserGroupId)
            .Select(x => new UserGroupRoleGridDTO
            {
                Id = x.Id,
                UserGroupId = x.UserGroupId,
                RoleId = x.RoleId,
                RoleRoleName = x.Role!.RoleName
            }).ToTableSettings(new PagedListFilterModel()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                FilterModelList = request.FilterModelList,
                OrderByColumnName = request.OrderByColumnName
            });
            return Result.SuccessDataResult(query);
        }, cancellationToken);
    }
}