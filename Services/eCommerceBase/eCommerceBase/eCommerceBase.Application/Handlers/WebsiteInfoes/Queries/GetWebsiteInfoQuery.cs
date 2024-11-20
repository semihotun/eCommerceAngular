using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Constant;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.SearchEngine;
using MediatR;

namespace eCommerceBase.Application.Handlers.WebsiteInfoes.Queries;
public record GetWebsiteInfoQuery() : IRequest<Result<WebsiteInfo>>;
public class GetWebsiteInfoQueryHandler(ICoreSearchEngineContext searchEngineContext,
		ICacheService cacheService) : IRequestHandler<GetWebsiteInfoQuery,
		Result<WebsiteInfo>>
{
    private readonly ICoreSearchEngineContext _searchEngineContext = searchEngineContext;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<WebsiteInfo>> Handle(GetWebsiteInfoQuery request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            string indexName = _searchEngineContext.IndexName<WebsiteInfo>();
            var existingDoc = await _searchEngineContext.Client.GetAsync<WebsiteInfo>(InitConst.WebSiteInfoId, g => g.Index(indexName));
            
            return Result.SuccessDataResult<WebsiteInfo>(existingDoc.Source!);
        },
		cancellationToken);
    }
}