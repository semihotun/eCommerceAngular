using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Constant;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.SearchEngine;
using MediatR;

namespace eCommerceBase.Application.Handlers.MailInfos.Queries;

public record GetMailInfoQuery() : IRequest<Result<MailInfo>>;
public class GetMailInfoQueryHandler(ICoreSearchEngineContext searchEngineContext,
        ICacheService cacheService) : IRequestHandler<GetMailInfoQuery,
        Result<MailInfo>>
{
    private readonly ICoreSearchEngineContext _searchEngineContext = searchEngineContext;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<MailInfo>> Handle(GetMailInfoQuery request,
        CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
        async () =>
        {
            string indexName = _searchEngineContext.IndexName<MailInfo>();
            var existingDoc = await _searchEngineContext.Client.GetAsync<MailInfo>(InitConst.MailInfoId, g => g.Index(indexName));

            return Result.SuccessDataResult(existingDoc.Source!);
        },
        cancellationToken);
    }
}