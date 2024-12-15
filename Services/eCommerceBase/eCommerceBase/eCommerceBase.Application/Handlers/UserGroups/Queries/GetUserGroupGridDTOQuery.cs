using eCommerceBase.Application.Handlers.UserGroups.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Persistence.GenericRepository;
using MediatR;

namespace eCommerceBase.Application.Handlers.UserGroups.Queries;
public record GetUserGroupGridDTOQuery(int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList)
    : IRequest<Result<PagedList<UserGroupGridDTO>>>;
public class GetUserGroupGridDTOQueryHandler(IReadDbRepository<UserGroup> userGroupRepository, ICacheService cacheService)
    : IRequestHandler<GetUserGroupGridDTOQuery, Result<PagedList<UserGroupGridDTO>>>
{
    private readonly ICacheService _cacheService = cacheService;
    private readonly IReadDbRepository<UserGroup> _userGroupRepository = userGroupRepository;
    public async Task<Result<PagedList<UserGroupGridDTO>>> Handle(GetUserGroupGridDTOQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request, async () =>
        {
            var query = await _userGroupRepository.Query().Select(x => new UserGroupGridDTO
            {
                Id = x.Id,
                Name = x.Name
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