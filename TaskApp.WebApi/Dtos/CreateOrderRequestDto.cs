namespace TaskApp.WebApi.Dtos;

public sealed record CreateOrderRequestDto(
    string CustomerName,
    string CustomerEmail,
    string CustomerCSM,
    List<ProductDetailDto> ProductDetails);
