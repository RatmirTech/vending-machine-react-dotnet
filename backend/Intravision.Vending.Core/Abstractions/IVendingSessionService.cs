namespace Intravision.Vending.Core.Abstractions;

/// <summary>
/// Сервис по работе с сесиями подключения к автоматам.
/// </summary>
public interface IVendingSessionService
{
    /// <summary>
    /// Попытаться занять автомат.
    /// Получить ИД сессии или null, если автомат уже занят
    /// </summary>
    string? StartSession(string machineKey, string connectionId);

    /// <summary>
    /// Освободить автомат
    /// </summary>
    void EndSession(string connectionId);

    /// <summary>
    /// Проверить, занят ли автомат
    /// </summary>
    bool IsLocked(string machineKey);

    /// <summary>
    /// Получить текущую сессию по ключу
    /// </summary>
    string? GetCurrentSession(string machineKey);
}