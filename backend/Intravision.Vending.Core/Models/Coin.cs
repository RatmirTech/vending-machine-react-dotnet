namespace Intravision.Vending.Core.Models;

/// <summary>
/// Представляет монету в системе (например, для выдачи сдачи).
/// </summary>
public class Coin
{
    /// <summary>
    /// Уникальный идентификатор монеты.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Номинал монеты (в условных единицах, напр. рубли).
    /// </summary>
    public int Denomination { get; set; }

    /// <summary>
    /// Количество монет данного номинала в наличии.
    /// </summary>
    public int Quantity { get; set; }
}

