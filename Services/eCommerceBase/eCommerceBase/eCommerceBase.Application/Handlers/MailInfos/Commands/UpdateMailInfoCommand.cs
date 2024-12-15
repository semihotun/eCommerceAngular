using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Constant;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.SearchEngine;
using Elasticsearch.Net;
using MediatR;

using Result = eCommerceBase.Domain.Result.Result;

namespace eCommerceBase.Application.Handlers.MailInfos.Commands;

public record UpdateMailInfoCommand(
       string FromAddress,
        string FromPassword,
       string Host,
        int Port) : IRequest<Result>;
public class UpdateMailInfoCommandHandler(ICoreSearchEngineContext searchEngineContext,
        ICacheService cacheService) : IRequestHandler<UpdateMailInfoCommand,
        Result>
{
    private readonly ICoreSearchEngineContext _searchEngineContext = searchEngineContext;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(UpdateMailInfoCommand request,
        CancellationToken cancellationToken)
    {
        string indexName = _searchEngineContext.IndexName<MailInfo>();
        var existingDoc = await _searchEngineContext.Client.GetAsync<MailInfo>(InitConst.MailInfoId, g => g.Index(indexName), cancellationToken);
        if (!existingDoc.Found)
        {
            return Result.ErrorResult(Messages.UpdatedError);
        }
        var updatedDoc = MailInfoMapper.UpdateMailInfoCommandToMailInfo(request);

        var response = await _searchEngineContext.Client.IndexAsync(updatedDoc, i => i
               .Index(indexName)
               .Id(InitConst.MailInfoId)
               .Refresh(Refresh.WaitFor)
        , cancellationToken);
        if (!response.IsValid)
        {
            return Result.ErrorResult(Messages.UpdatedError);
        }
        await _cacheService.RemovePatternAsync("eCommerceBase:MailInfo", cancellationToken);
        return Result.SuccessResult(Messages.Updated);
    }
}