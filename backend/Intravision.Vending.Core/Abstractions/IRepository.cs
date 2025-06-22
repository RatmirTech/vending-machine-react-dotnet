namespace Intravision.Vending.Core.Abstractions;

/// <summary>
/// Универсальный интерфейс репозитория для работы с сущностями.
/// Обеспечивает базовые операции CRUD и дополнительные методы доступа к данным.
/// </summary>
/// <typeparam name="T">Тип сущности.</typeparam>
/// <typeparam name="TKey">Тип первичного ключа сущности. Должен быть notnull.</typeparam>
public interface IRepository<T, in TKey>
    where T : class
    where TKey : notnull
{
    /// <summary>
    /// Асинхронно получает все сущности.
    /// </summary>
    /// <param name="token">Токен отмены асинхронной операции.</param>
    /// <returns>Коллекция всех сущностей типа <typeparamref name="T"/>.</returns>
    Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default);

    /// <summary>
    /// Асинхронно получает сущность по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор сущности.</param>
    /// <param name="token">Токен отмены асинхронной операции.</param>
    /// <returns>Сущность типа <typeparamref name="T"/>, либо <c>null</c>, если не найдена.</returns>
    Task<T?> GetByIdAsync(TKey id, CancellationToken token = default);

    /// <summary>
    /// Асинхронно создаёт новую сущность.
    /// </summary>
    /// <param name="entity">Создаваемая сущность.</param>
    /// <param name="token">Токен отмены асинхронной операции.</param>
    /// <returns>Созданная сущность.</returns>
    Task<T> CreateAsync(T entity, CancellationToken token = default);

    /// <summary>
    /// Асинхронно обновляет существующую сущность.
    /// </summary>
    /// <param name="entity">Обновляемая сущность.</param>
    /// <param name="token">Токен отмены асинхронной операции.</param>
    /// <returns>Обновлённая сущность.</returns>
    Task<T> UpdateAsync(T entity, CancellationToken token = default);

    /// <summary>
    /// Асинхронно удаляет сущность по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемой сущности.</param>
    /// <param name="token">Токен отмены асинхронной операции.</param>
    Task DeleteAsync(TKey id, CancellationToken token = default);

    /// <summary>
    /// Проверяет существование сущности с заданным идентификатором.
    /// </summary>
    /// <param name="id">Идентификатор проверяемой сущности.</param>
    /// <param name="token">Токен отмены асинхронной операции.</param>
    /// <returns><c>true</c>, если сущность существует; иначе — <c>false</c>.</returns>
    Task<bool> ExistsAsync(TKey id, CancellationToken token = default);

    /// <summary>
    /// Асинхронно выполняет поиск сущностей по заданному предикату.
    /// </summary>
    /// <param name="predicate">Функция фильтрации.</param>
    /// <param name="token">Токен отмены асинхронной операции.</param>
    /// <returns>Коллекция сущностей, удовлетворяющих предикату.</returns>
    Task<IEnumerable<T>> FindAsync(Func<T, bool> predicate, CancellationToken token = default);

    /// <summary>
    /// Асинхронно сохраняет изменения в источнике данных.
    /// </summary>
    /// <param name="token">Токен отмены асинхронной операции.</param>
    Task SaveChangesAsync(CancellationToken token = default);
}