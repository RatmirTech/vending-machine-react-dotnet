using Intravision.Vending.Core.Abstractions;
using Intravision.Vending.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace Intravision.Vending.DAL.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly EfContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(EfContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<T>();
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken token = default)
    {
        T? entity = await GetByIdAsync(id);
        return entity != null;
    }

    public Task<IEnumerable<T>> FindAsync(Func<T, bool> predicate, CancellationToken token = default)
    {
        IEnumerable<T> entities = _dbSet.AsEnumerable().Where(predicate);
        return Task.FromResult(entities);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default)
    {
        IEnumerable<T> entities = await _dbSet.ToListAsync(token);
        return entities;
    }

    public async Task<T?> GetByIdAsync(string id, CancellationToken token = default)
    {
        T? entity = await _dbSet.FindAsync(ConvertId(id), token);
        return entity;
    }

    public async Task<T> CreateAsync(T entity, CancellationToken token = default)
    {
        await _dbSet.AddAsync(entity, token);
        return entity;
    }

    public async Task DeleteAsync(string id, CancellationToken token = default)
    {
        T? entity = await GetByIdAsync(id);
        if (entity != null)
            _dbSet.Remove(entity);
    }

    public Task<T> UpdateAsync(T entity, CancellationToken token = default)
    {
        _dbSet.Update(entity);
        return Task.FromResult(entity);
    }

    public async Task SaveChangesAsync(CancellationToken token = default)
    {
        await _context.SaveChangesAsync(token);
    }

    private static object ConvertId(string id)
    {
        var type = typeof(T).GetProperty("Id")?.PropertyType;

        if (type == typeof(Guid))
            return Guid.Parse(id);

        if (type == typeof(int))
            return int.Parse(id);

        return id;
    }
}
