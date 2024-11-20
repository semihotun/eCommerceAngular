using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Constant;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.SearchEngine;
using eCommerceBase.Persistence.UnitOfWork;
using Elasticsearch.Net;
using MediatR;

using Result = eCommerceBase.Domain.Result.Result;

namespace eCommerceBase.Application.Handlers.WebsiteInfoes.Commands;

public record UpdateWebsiteInfoCommand(
        string? SocialMediaText,
        string? Logo,
        string? WebSiteName,
        List<SocialMediaInfo>? SocialMediaInfos) : IRequest<Result>;
public class UpdateWebsiteInfoCommandHandler(ICoreSearchEngineContext searchEngineContext,
        IUnitOfWork unitOfWork,
        ICacheService cacheService) : IRequestHandler<UpdateWebsiteInfoCommand,
        Result>
{
    private readonly ICoreSearchEngineContext _searchEngineContext = searchEngineContext;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(UpdateWebsiteInfoCommand request,
        CancellationToken cancellationToken)
    {
        string indexName = _searchEngineContext.IndexName<WebsiteInfo>();
        var existingDoc = await _searchEngineContext.Client.GetAsync<WebsiteInfo>(InitConst.WebSiteInfoId, g => g.Index(indexName));
        if (!existingDoc.Found)
        {
            return Result.ErrorResult(Messages.UpdatedError);
        }
        var updatedDoc = WebsiteInfoMapper.UpdateWebsiteInfoCommandToWebsiteInfo(request);

        var response = await _searchEngineContext.Client.IndexAsync(updatedDoc, i => i
               .Index(indexName)
               .Id(InitConst.WebSiteInfoId)
               .Refresh(Refresh.WaitFor)
           );

        if (!response.IsValid)
        {
            return Result.ErrorResult(Messages.UpdatedError);
        }

        await _cacheService.RemovePatternAsync("eCommerceBase:WebsiteInfo");
        return Result.SuccessResult(Messages.Updated);

    }
}