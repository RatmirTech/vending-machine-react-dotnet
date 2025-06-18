namespace Intravision.Vending.Core.Abstractions;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default);

    Task<T?> GetByIdAsync(string id, CancellationToken token = default);

    Task<T> CreateAsync(T entity, CancellationToken token = default);

    Task<T> UpdateAsync(T entity, CancellationToken token = default);

    Task DeleteAsync(string id, CancellationToken token = default);

    Task<bool> ExistsAsync(string id, CancellationToken token = default);

    Task<IEnumerable<T>> FindAsync(Func<T, bool> predicate, CancellationToken token = default);

    Task SaveChangesAsync(CancellationToken token = default);
}
