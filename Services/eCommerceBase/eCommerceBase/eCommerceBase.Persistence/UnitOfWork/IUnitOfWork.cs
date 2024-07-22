using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.SeedWork;
using eCommerceBase.Insfrastructure.Utilities.Outboxes;

namespace eCommerceBase.Persistence.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<T> BeginTransaction<T>(Func<Task<T>> action)
        where T : IResult;
        Task<T> BeginTransactionAndCreateOutbox<T>(Func<Action<IOutboxMessage>, Task<T>> action)
           where T : Result;
        Task BeginTransactionAndCreateOutbox(Func<Action<IOutboxMessage>, Task> action);
        Task DispatchDomainEventsOutboxAsync(IOutboxMessage message);
    }
}
