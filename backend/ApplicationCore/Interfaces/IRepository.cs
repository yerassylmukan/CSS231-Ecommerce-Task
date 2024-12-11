using System.Linq.Expressions;

namespace ApplicationCore.Interfaces;

public interface IRepository<T> where T : class
{
    Task<List<T>> ListAsync(CancellationToken cancellationToken);
    Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate);
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);
    Task DeleteAsync(T entity, CancellationToken cancellationToken);
}