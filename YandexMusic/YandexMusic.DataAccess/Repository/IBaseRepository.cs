

using System.Linq.Expressions;

namespace YandexMusic.DataAccess.Repository;
public interface IBaseRepository<TEntity> where TEntity :
{
    Task<TEntity> CreateAsync(Expression<Func<TEntity, bool>> predicate);
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity , bool>> predicate);
    IQueryable<TEntity> GetAll();
    IEnumerable<TEntity> GetAllAsEnumerable();
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(TEntity entity);
}
