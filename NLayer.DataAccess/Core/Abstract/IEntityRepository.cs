using System.Linq.Expressions;

namespace NLayer.DataAccess.Core.Abstract
{
    public interface IEntityRepository<T>
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        IQueryable<T> GetAllAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> filter);
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class;
    }
}
