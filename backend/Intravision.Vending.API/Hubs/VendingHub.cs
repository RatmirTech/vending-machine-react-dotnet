using Intravision.Vending.Core.Abstractions;
using Microsoft.AspNetCore.SignalR;

namespace Intravision.Vending.API.Hubs;

public class VendingHub : Hub
{
    private readonly IVendingSessionService _sessionService;

    private readonly ILogger<VendingHub> _logger;

    private const string MachineKey = "vm1";

    public VendingHub(IVendingSessionService sessionService,
        ILogger<VendingHub> logger)
    {
        _sessionService = sessionService;
        _logger = logger;
    }

    public override async Task OnConnectedAsync()
    {

        _logger.LogInformation($"{nameof(VendingHub)} попытка подключения");

        var http = Context.GetHttpContext();

        if (_sessionService.IsLocked(MachineKey))
        {
            await Clients.Caller.SendAsync("Notify", "Извините, в данный момент автомат занят");
            Context.Abort();
            return;
        }

        var sessionId = _sessionService.StartSession(MachineKey, Context.ConnectionId);
        if (sessionId is null)
        {
            await Clients.Caller.SendAsync("Notify", "Не удалось начать сессию");
            Context.Abort();
            return;
        }

        await Clients.Caller.SendAsync("SessionStarted", sessionId);
        _logger.LogInformation($"{nameof(VendingHub)} {sessionId} подключён");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        string connectionId = Context.ConnectionId;
        _sessionService.EndSession(connectionId);

        _logger.LogInformation($"{connectionId} отключён от автомата");

        await base.OnDisconnectedAsync(exception);
    }
}
