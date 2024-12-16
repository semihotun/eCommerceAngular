using eCommerceBase.Domain.SeedWork;
using eCommerceBase.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eCommerceBase.Persistence.GenericRepository
{
    public class ReadDbRepository<TEntity> : IReadDbRepository<TEntity>
    where TEntity : BaseEntity, IEntity
    {
        private readonly ICoreDbReadContext _readContext;
        public ReadDbRepository(ICoreDbReadContext readContext)
        {
            _readContext = readContext;
        }
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _readContext.Query<TEntity>().AnyAsync(expression);
        }
        public async Task<TEntity?> GetByIdAsync(Guid Id)
        {
            var entity = await _readContext.Query<TEntity>().FirstOrDefaultAsync(x => x.Id == Id);
            if (entity?.Deleted == false)
            {
                return entity;
            }
            return null;
        }
        #region Get 
        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _readContext.Query<TEntity>().FirstOrDefaultAsync(expression);
        }
        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _readContext.Query<TEntity>();
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
            return await _readContext.Query<TEntity>().ToListAsync();
        }
        public async Task<IList<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _readContext.Query<TEntity>().Where(expression).ToListAsync();
        }
        public async Task<IList<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _readContext.Query<TEntity>();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.Where(expression).ToListAsync();
        }
        public async Task<IList<TEntity>> ToListAsync(Expression<Func<TEntity, bool>>? expression = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _readContext.Query<TEntity>();
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
            return await _readContext.Query<TEntity>().CountAsync(expression);
        }
        public async Task<int> GetCountAsync()
        {
            return await _readContext.Query<TEntity>().CountAsync();
        }
        #endregion
        public IQueryable<TEntity> Query() => _readContext.Query<TEntity>();
    }
}
