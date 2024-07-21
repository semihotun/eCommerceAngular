using eCommerceBase.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace eCommerceBase.Persistence.Context
{
    public class CoreDbReadContext : DbContext
    {
        public CoreDbReadContext(DbContextOptions<CoreDbReadContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public IQueryable<TEntity> Query<TEntity>() where TEntity : BaseEntity
        {
            return Set<TEntity>().AsQueryable().Where(x => !x.Deleted);
        }
    }
}
