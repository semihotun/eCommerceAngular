using MediatR;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Domain.Result;
using eCommerceBase.Application.Handlers.UserGroupRoles.Queries.Dtos;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.Context;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;
using Microsoft.EntityFrameworkCore;

namespace eCommerceBase.Application.Handlers.UserGroupRoles.Queries;
public record GetUserGroupRoleGridDTOQuery(Guid UserGroupId, int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList) : IRequest<Result<PagedList<UserGroupRoleGridDTO>>>;
public class GetUserGroupRoleGridDTOQueryHandler(CoreDbContext coreDbContext, ICacheService cacheService) : IRequestHandler<GetUserGroupRoleGridDTOQuery, Result<PagedList<UserGroupRoleGridDTO>>>
{
    private readonly CoreDbContext _coreDbContext = coreDbContext;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<UserGroupRoleGridDTO>>> Handle(GetUserGroupRoleGridDTOQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync<Result<PagedList<UserGroupRoleGridDTO>>>(request, async () =>
        {
            var query = await _coreDbContext.Query<UserGroupRole>()
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
            return Result.SuccessDataResult<PagedList<UserGroupRoleGridDTO>>(query);
        }, cancellationToken);
    }
}