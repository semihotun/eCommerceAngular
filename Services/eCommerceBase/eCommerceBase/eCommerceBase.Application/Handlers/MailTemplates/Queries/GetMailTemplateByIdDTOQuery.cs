using MediatR;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Domain.Result;
using eCommerceBase.Application.Handlers.MailTemplates.Queries.Dtos;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.Context;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;
using Microsoft.EntityFrameworkCore;

namespace eCommerceBase.Application.Handlers.MailTemplates.Queries;
public record GetMailTemplateByIdDTOQuery(Guid id) : IRequest<Result<MailTemplateByIdDTO>>;
public class GetMailTemplateByIdDTOQueryHandler(CoreDbContext coreDbContext,
    ICacheService cacheService) : IRequestHandler<GetMailTemplateByIdDTOQuery, Result<MailTemplateByIdDTO>>
{
    private readonly CoreDbContext _coreDbContext = coreDbContext;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<MailTemplateByIdDTO>> Handle(GetMailTemplateByIdDTOQuery request,
        CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request, async () =>
        {
            var query = await _coreDbContext.Query<MailTemplate>().Include(x => x.MailTemplateKeywordList
            ).Where(x=> x.Id==request.id).Select(x => new MailTemplateByIdDTO
            {
                Id = x.Id,
                TemplateHeader = x.TemplateHeader,
                TemplateContent = x.TemplateContent,
                MailTemplateKeywordList = x.MailTemplateKeywordList
            }).FirstOrDefaultAsync();

            return Result.SuccessDataResult(query!);
        }, cancellationToken);
    }
}