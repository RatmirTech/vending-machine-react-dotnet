namespace Intravision.Vending.Core.DTO.Order;

public class OrderCreateResponse
{
    public string Message { get; set; } = string.Empty;

    public bool Success { get; set; }

    public int ChangeAmount { get; set; }
}
