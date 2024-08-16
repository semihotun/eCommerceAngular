using eCommerceBase.Domain.SeedWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace eCommerceBase.Persistence.Context
{
    /// <summary>
    /// Custom db context
    /// </summary>
    public class CoreDbContext(DbContextOptions<CoreDbContext> options, IHttpContextAccessor? httpContextAccessor) : DbContext(options), ICoreDbContext
    {
        private readonly IHttpContextAccessor? _httpContextAccessor = httpContextAccessor;
        public const string DEFAULT_SCHEMA = "CoreDbContextSchema";
        private string _languageCode = "tr";
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public IQueryable<TEntity> Query<TEntity>() where TEntity : BaseEntity
        {
            if (!String.IsNullOrEmpty(_httpContextAccessor?.HttpContext?.Request.Headers["LanguageCode"].ToString()))
            {
                _languageCode = _httpContextAccessor?.HttpContext?.Request.Headers["LanguageCode"].ToString()!;
            }
            return Set<TEntity>().AsQueryable().AsNoTracking().Where(x => !x.Deleted && x.LanguageCode == _languageCode);
        }
        public IQueryable<TEntity> WriteQuery<TEntity>() where TEntity : BaseEntity
        {
            return Set<TEntity>().AsQueryable().AsNoTracking().Where(x => !x.Deleted);
        }
    }
}
