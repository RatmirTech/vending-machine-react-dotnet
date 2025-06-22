using Intravision.Vending.Core.Abstractions;
using System.Collections.Concurrent;

namespace Intravision.Vending.Core.Services;

/// <summary>
/// Сервис управления сессиями пользователя в торговом автомате.
/// Используется для хранения состояния взаимодействия
/// </summary>
public class VendingSessionService : IVendingSessionService
{
    /// <summary>
    /// Потокобезопасный словарь для хранения сессий.
    /// </summary>
    private static readonly ConcurrentDictionary<string, string> _sessions = new();

    public string? StartSession(string machineKey, string connectionId)
    {
        if (!_sessions.TryAdd(machineKey, connectionId))
            return null;
        return connectionId;
    }

    public void EndSession(string connectionId)
    {
        var item = _sessions.FirstOrDefault(x => x.Value == connectionId);
        if (!item.Equals(default(KeyValuePair<string, string>)))
        {
            _sessions.TryRemove(item.Key, out _);
        }
    }

    public bool IsLocked(string machineKey)
        => _sessions.ContainsKey(machineKey);

    public string? GetCurrentSession(string machineKey)
        => _sessions.TryGetValue(machineKey, out var id) ? id : null;
}