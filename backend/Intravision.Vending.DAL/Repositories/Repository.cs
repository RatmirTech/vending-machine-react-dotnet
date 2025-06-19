using Intravision.Vending.Core.Abstractions;
using Intravision.Vending.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace Intravision.Vending.DAL.Repositories;

public class Repository<T, TKey> : IRepository<T, TKey>
    where T : class
    where TKey : notnull
{
    protected readonly EfContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(EfContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default)
    {
        return await _dbSet.ToListAsync(token);
    }

    public async Task<T?> GetByIdAsync(TKey id, CancellationToken token = default)
    {
        return await _dbSet.FindAsync(new object[] { id }, token);
    }

    public async Task<T> CreateAsync(T entity, CancellationToken token = default)
    {
        await _dbSet.AddAsync(entity, token);
        return entity;
    }

    public Task<T> UpdateAsync(T entity, CancellationToken token = default)
    {
        _dbSet.Update(entity);
        return Task.FromResult(entity);
    }

    public async Task DeleteAsync(TKey id, CancellationToken token = default)
    {
        var entity = await GetByIdAsync(id, token);
        if (entity != null)
            _dbSet.Remove(entity);
    }

    public async Task<bool> ExistsAsync(TKey id, CancellationToken token = default)
    {
        var entity = await GetByIdAsync(id, token);
        return entity != null;
    }

    public Task<IEnumerable<T>> FindAsync(Func<T, bool> predicate, CancellationToken token = default)
    {
        return Task.FromResult(_dbSet.AsEnumerable().Where(predicate));
    }

    public async Task SaveChangesAsync(CancellationToken token = default)
    {
        await _context.SaveChangesAsync(token);
    }
}
