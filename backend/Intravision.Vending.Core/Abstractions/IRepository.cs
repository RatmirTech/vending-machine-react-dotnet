namespace Intravision.Vending.Core.Abstractions;

public interface IRepository<T, in TKey>
    where T : class
    where TKey : notnull
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default);

    Task<T?> GetByIdAsync(TKey id, CancellationToken token = default);

    Task<T> CreateAsync(T entity, CancellationToken token = default);

    Task<T> UpdateAsync(T entity, CancellationToken token = default);

    Task DeleteAsync(TKey id, CancellationToken token = default);

    Task<bool> ExistsAsync(TKey id, CancellationToken token = default);

    Task<IEnumerable<T>> FindAsync(Func<T, bool> predicate, CancellationToken token = default);

    Task SaveChangesAsync(CancellationToken token = default);
}
