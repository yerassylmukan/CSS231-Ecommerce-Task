using System.Linq.Expressions;
using ApplicationCore.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _dbContext;

    public Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbContext.Set<T>()
            .Where(predicate)
            .ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().FindAsync([id], cancellationToken);
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}