using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using NLayer.DataAccess.Concrete.EntityFramework;
using NLayer.DataAccess.Core.Abstract;
using NLayer.Entity.Abstract;

namespace NLayer.DataAccess.Core.Concrete
{
    public class GenericRepository<TEntity> : IEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private EfContext _context;

        public GenericRepository(EfContext context)
        {
            _context = context;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(filter);
        }

        public IQueryable<TEntity> GetAllAsync()
        {
            return _context.Set<TEntity>().AsNoTracking();
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> filter)
        {
            return _context.Set<TEntity>().AsNoTracking().Where(filter);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(x => x.Id == id);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
