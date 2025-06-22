namespace Intravision.Vending.Core.DTO.Order;

/// <summary>
/// Ответ на создание заказа, содержащий информацию об успешности, сообщении и сдаче.
/// </summary>
public class OrderCreateResponse
{
    /// <summary>
    /// Информационное сообщение о результате создания заказа.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Указывает, был ли заказ успешно создан.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Сумма сдачи, которую необходимо вернуть покупателю.
    /// </summary>
    public int ChangeAmount { get; set; }

    /// <summary>
    /// Сдача, разложенная по монетам:
    /// ключ — номинал монеты, значение — количество таких монет.
    /// </summary>
    public Dictionary<int, int> ChangeToGive { get; set; } = new();
}
