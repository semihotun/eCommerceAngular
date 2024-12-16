using eCommerceBase.Domain.SeedWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace eCommerceBase.Persistence.Context
{
    public class CoreDbReadContext(DbContextOptions<CoreDbReadContext> options, IHttpContextAccessor? httpContextAccessor)
        : DbContext(options), ICoreDbReadContext
    {
        private readonly IHttpContextAccessor? _httpContextAccessor = httpContextAccessor;
        public const string DEFAULT_SCHEMA = "CoreDbReadContextSchema";
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
        public IQueryable<TEntity> NotLanguageQuery<TEntity>() where TEntity : BaseEntity
        {
            return Set<TEntity>().AsQueryable().AsNoTracking().Where(x => !x.Deleted);
        }
    }
}
