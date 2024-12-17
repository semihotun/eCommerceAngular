using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Persistence.SearchEngine;
using MediatR;

namespace eCommerceBase.Application.Handlers.ProductSearches.Queries
{
    public record GetHomeProductSearchQuery(string ProductName) : IRequest<Result<List<ProductSearch>>>;
    public class GetHomeProductSearchQueryHandler(ICoreSearchEngineContext searchEngineContext) : IRequestHandler<GetHomeProductSearchQuery,
        Result<List<ProductSearch>>>
    {
        private readonly ICoreSearchEngineContext _searchEngineContext = searchEngineContext;
        public async Task<Result<List<ProductSearch>>> Handle(GetHomeProductSearchQuery request,
            CancellationToken cancellationToken)
        {
            var indexName = _searchEngineContext.IndexName<ProductSearch>();
            var searchResponse = await _searchEngineContext.Client.SearchAsync<ProductSearch>(s => s
               .Index(indexName)
               .Query(q => q
                   .Prefix(p => p
                       .Field(f => f.ProductName)
                       .Value(request.ProductName.ToLower())
                   )        
               ).Size(10)
           ,cancellationToken);
            var data = searchResponse.Documents.ToList();
            return Result.SuccessDataResult(data);
        }
    }
}
