namespace TaskApp.WebApi.Dtos;

public sealed record ProductDetailDto(
    string ProductId,
    decimal UnitPrice,
    decimal Quantity);
