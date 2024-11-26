using eCommerceBase.Domain.SeedWork;
using eCommerceBase.Persistence.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eCommerceBase.Persistence.GenericRepository
{
    public class WriteDbRepository<TEntity> : IWriteDbRepository<TEntity>
    where TEntity : BaseEntity, IEntity
    {
        private readonly ICoreDbContext _writeContext;
        private readonly IHttpContextAccessor? _httpContextAccessor;
        private string _languageCode = "tr";
        public WriteDbRepository(ICoreDbContext writeContext, IHttpContextAccessor httpContextAccessor)
        {
            _writeContext = writeContext;
            _httpContextAccessor = httpContextAccessor;
            if (!String.IsNullOrEmpty(_httpContextAccessor?.HttpContext?.Request.Headers["LanguageCode"].ToString()))
            {
                _languageCode = _httpContextAccessor?.HttpContext?.Request.Headers["LanguageCode"].ToString()!;
            }
        }
        public void AddRange(List<TEntity> entity)
        {
            foreach (var data in entity)
            {
                data.CreatedOnUtc = DateTime.Now;
                data.LanguageCode= _languageCode;
            }
            _writeContext.Set<TEntity>().AddRange(entity);
        }
        public async Task AddRangeAsync(IEnumerable<TEntity> entity)
        {
            foreach (var data in entity)
            {
                data.CreatedOnUtc = DateTime.Now;
                data.LanguageCode = _languageCode;
            }
            await _writeContext.Set<TEntity>().AddRangeAsync(entity);
        }
        public TEntity Update(TEntity entity)
        {
            entity.UpdatedOnUtc = DateTime.Now;
            entity.LanguageCode = _languageCode;
            _writeContext.Set<TEntity>().Update(entity);
            return entity;
        }
        public void Remove(TEntity entity)
        {
            entity.Deleted = true;
            _writeContext.Set<TEntity>().Update(entity);
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.CreatedOnUtc = DateTime.Now;
            entity.LanguageCode = _languageCode;
            return (await _writeContext.Set<TEntity>().AddAsync(entity)).Entity;
        }
        public void RemoveRange(List<TEntity> entity)
        {
            foreach (var item in entity)
            {
                Remove(item);
            }
        }
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _writeContext.WriteQuery<TEntity>().AnyAsync(expression);
        }
        public async Task<TEntity?> GetByIdAsync(Guid Id)
        {
            var entity = await _writeContext.Set<TEntity>().FindAsync(Id);
            if (entity?.Deleted == false)
            {
                return entity;
            }
            return null;
        }
        #region Get 
        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _writeContext.WriteQuery<TEntity>().FirstOrDefaultAsync(expression);
        }
        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _writeContext.WriteQuery<TEntity>();
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            return await query.FirstOrDefaultAsync(expression);
        }
        #endregion
        #region ToListAsync
        public async Task<IList<TEntity>> ToListAsync()
        {
            return await _writeContext.WriteQuery<TEntity>().ToListAsync();
        }
        public async Task<IList<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _writeContext.WriteQuery<TEntity>().Where(expression).ToListAsync();
        }
        public async Task<IList<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _writeContext.WriteQuery<TEntity>();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.Where(expression).ToListAsync();
        }
        public async Task<IList<TEntity>> ToListAsync(Expression<Func<TEntity, bool>>? expression = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _writeContext.WriteQuery<TEntity>();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return await query.ToListAsync();
        }
        #endregion
        #region GetCountAsync
        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _writeContext.WriteQuery<TEntity>().CountAsync(expression);
        }
        public async Task<int> GetCountAsync()
        {
            return await _writeContext.WriteQuery<TEntity>().CountAsync();
        }
        #endregion
        public IQueryable<TEntity> Query() => _writeContext.WriteQuery<TEntity>();
    }
}
