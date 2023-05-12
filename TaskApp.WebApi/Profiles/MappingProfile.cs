using AutoMapper;
using TaskApp.WebApi.Dtos;
using TaskApp.WebApi.Models;

namespace TaskApp.WebApi.Profiles;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateOrderRequestDto, Order>();
        CreateMap<ProductDetailDto, OrderDetail>();
    }
}
