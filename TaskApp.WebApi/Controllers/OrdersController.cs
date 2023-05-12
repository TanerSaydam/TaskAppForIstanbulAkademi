using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskApp.WebApi.Context;
using TaskApp.WebApi.Dtos;
using TaskApp.WebApi.Models;
using TaskApp.WebApi.Results;

namespace TaskApp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public OrdersController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> CreateOrder(CreateOrderRequestDto request, CancellationToken cancellationToken)
    {
        //Order order = new()
        //{
        //    CustomerName = request.CustomerName,
        //    CustomerGSM = request.CustomerCSM,
        //    CustomerEmail = request.CustomerEmail,
        //    TotalAmount = request.ProductDetails.Sum(s => s.UnitPrice * s.Quantity),
        //    CreatedDate = DateTime.Now,
        //};
        Order order = _mapper.Map<Order>(request);
        order.CreatedDate = DateTime.Now;
        order.TotalAmount = request.ProductDetails.Sum(s => s.UnitPrice * s.Quantity);

        await _context.Set<Order>().AddAsync(order, cancellationToken);

        List<OrderDetail> orderDetails = new();
        foreach (var detail in request.ProductDetails)
        {
            OrderDetail orderDetail = _mapper.Map<OrderDetail>(detail);
            orderDetail.CreatedDate = DateTime.Now;
            orderDetail.OrderId = order.Id;

            //OrderDetail orderDetail = new()
            //{
            //    OrderId = order.Id,
            //    ProductId = detail.ProductId,
            //    CreatedDate = DateTime.Now,
            //    Quantity = detail.Quantity,
            //    UnitPrice = detail.UnitPrice,
            //};
            orderDetails.Add(orderDetail);
        }

        await _context.Set<OrderDetail>().AddRangeAsync(orderDetails, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        ApiResponse<string> response = new(StatusType.Success, order.Id);
        return Ok(response);
    }
}
