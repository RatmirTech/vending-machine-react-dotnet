namespace Intravision.Vending.Core.DTO.Brand;

/// <summary>
/// DTO-ответ, содержащий информацию о бренде.
/// </summary>
/// <param name="Id">Уникальный идентификатор бренда.</param>
/// <param name="Name">Название бренда.</param>
public record BrandGetResponse(Guid Id, string Name);
