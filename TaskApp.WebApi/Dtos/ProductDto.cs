namespace TaskApp.WebApi.Dtos;

public sealed record ProductDto(
    string Id,
    string Description,
    string Category,
    decimal Unit,
    decimal UnitPrice);

