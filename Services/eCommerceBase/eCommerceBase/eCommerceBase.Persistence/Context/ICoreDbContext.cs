using eCommerceBase.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace eCommerceBase.Persistence.Context
{
    public interface ICoreDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        IQueryable<TEntity> Query<TEntity>() where TEntity : BaseEntity;
        IQueryable<TEntity> WriteQuery<TEntity>() where TEntity : BaseEntity;
        DatabaseFacade Database { get; }
        ChangeTracker ChangeTracker { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
