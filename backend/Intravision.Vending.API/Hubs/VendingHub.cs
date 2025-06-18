using Intravision.Vending.Core.Abstractions;
using Microsoft.AspNetCore.SignalR;

namespace Intravision.Vending.API.Hubs;

public class VendingHub : Hub
{
    private readonly IVendingSessionService _sessionService;

    private readonly ILogger<VendingHub> _logger;

    public VendingHub(IVendingSessionService sessionService,
        ILogger<VendingHub> logger)
    {
        _sessionService = sessionService;
        _logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
        var http = Context.GetHttpContext();
        var machineKey = http?.Request.Query["machineKey"].ToString();

        if (string.IsNullOrEmpty(machineKey) || !IsAvailableMachine(machineKey))
        {
            await Clients.Caller.SendAsync("Notify", "Неверный ИД автомата");
            Context.Abort();
            return;
        }

        if (_sessionService.IsLocked(machineKey))
        {
            await Clients.Caller.SendAsync("Notify", "Извините, в данный момент автомат занят");
            Context.Abort();
            return;
        }

        var sessionId = _sessionService.StartSession(machineKey, Context.ConnectionId);
        if (sessionId is null)
        {
            await Clients.Caller.SendAsync("Notify", "Не удалось начать сессию");
            Context.Abort();
            return;
        }

        await Clients.Caller.SendAsync("SessionStarted", sessionId);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        string connectionId = Context.ConnectionId;
        _sessionService.EndSession(connectionId);

        _logger.LogInformation($"{connectionId} отключён от автомата");

        await base.OnDisconnectedAsync(exception);
    }

    public static bool IsAvailableMachine(string? machineKey)
    {
        return !string.IsNullOrEmpty(machineKey) && Machines.Contains(machineKey);
    }

    private static readonly List<string> Machines = new()
    {
        "Ven1"
    };
}
