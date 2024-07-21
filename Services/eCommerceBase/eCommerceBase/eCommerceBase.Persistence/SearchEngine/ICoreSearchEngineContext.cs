using eCommerceBase.Domain.SeedWork;
using Nest;

namespace eCommerceBase.Persistence.SearchEngine
{
    public interface ICoreSearchEngineContext
    {
        ElasticClient Client { get; }
        string IndexName<T>() where T : IElasticEntity;
    }
}
