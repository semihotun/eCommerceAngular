using eCommerceBase.Application.Handlers.Roles.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerceBase.Application.Handlers.Roles.Queries;
public record GetUserGroupNotExistRoleGridDTOQuery(Guid UserGroupId,int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList)
    : IRequest<Result<PagedList<GetUserGroupNotExistRoleGridDTO>>>;
public class GetUserGroupNotExistRoleGridDTOQueryHandler(CoreDbContext coreDbContext, ICacheService cacheService) 
    : IRequestHandler<GetUserGroupNotExistRoleGridDTOQuery, Result<PagedList<GetUserGroupNotExistRoleGridDTO>>>
{
    private readonly CoreDbContext _coreDbContext = coreDbContext;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<GetUserGroupNotExistRoleGridDTO>>> Handle(GetUserGroupNotExistRoleGridDTOQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync<Result<PagedList<GetUserGroupNotExistRoleGridDTO>>>(request, async () =>
        {
            var existingRoleIds = await _coreDbContext.WriteQuery<UserGroupRole>()
                .Where(x => x.UserGroupId == request.UserGroupId)
                .Select(x => x.RoleId)
                .ToListAsync(cancellationToken);

            var query = await _coreDbContext.Query<Role>()
             .Where(role => !existingRoleIds.Contains(role.Id))
            .Select(x => new GetUserGroupNotExistRoleGridDTO
            {
                Id = x.Id,
                RoleName = x.RoleName
            }).ToTableSettings(new PagedListFilterModel()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                FilterModelList = request.FilterModelList,
                OrderByColumnName = request.OrderByColumnName
            });
            return Result.SuccessDataResult<PagedList<GetUserGroupNotExistRoleGridDTO>>(query);
        }, cancellationToken);
    }
}