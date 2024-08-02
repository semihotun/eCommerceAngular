using MediatR;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Domain.Result;
using eCommerceBase.Application.Handlers.MailTemplates.Queries.Dtos;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.Context;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;

namespace eCommerceBase.Application.Handlers.MailTemplates.Queries;
public record GetMailTemplateGridDTOQuery(int PageIndex, int PageSize, string? OrderByColumnName,
    List<FilterModel>? FilterModelList) : IRequest<Result<PagedList<MailTemplateGridDTO>>>;
public class GetMailTemplateGridDTOQueryHandler(CoreDbContext coreDbContext,
    ICacheService cacheService) : IRequestHandler<GetMailTemplateGridDTOQuery, Result<PagedList<MailTemplateGridDTO>>>
{
    private readonly CoreDbContext _coreDbContext = coreDbContext;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<MailTemplateGridDTO>>> Handle(GetMailTemplateGridDTOQuery request,
        CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request, async () =>
        {
            var query = await _coreDbContext.Query<MailTemplate>().Select(x => new MailTemplateGridDTO
            {
                Id = x.Id,
                TemplateHeader = x.TemplateHeader
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