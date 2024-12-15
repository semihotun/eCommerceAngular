using eCommerceBase.Application.Handlers.MailTemplates.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Persistence.GenericRepository;
using MediatR;

namespace eCommerceBase.Application.Handlers.MailTemplates.Queries;
public record GetMailTemplateGridDTOQuery(int PageIndex, int PageSize, string? OrderByColumnName,
    List<FilterModel>? FilterModelList) : IRequest<Result<PagedList<MailTemplateGridDTO>>>;
public class GetMailTemplateGridDTOQueryHandler(IReadDbRepository<MailTemplate> mailTemplateRepository,
    ICacheService cacheService) : IRequestHandler<GetMailTemplateGridDTOQuery, Result<PagedList<MailTemplateGridDTO>>>
{
    private readonly IReadDbRepository<MailTemplate> _mailTemplateRepository = mailTemplateRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<MailTemplateGridDTO>>> Handle(GetMailTemplateGridDTOQuery request,
        CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request, async () =>
        {
            var query = await _mailTemplateRepository.Query().Select(x => new MailTemplateGridDTO
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