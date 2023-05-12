using TaskApp.WebApi.Abstraction;

namespace TaskApp.WebApi.Models;

public sealed class Order : Entity
{
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerGSM { get; set; }
    public decimal TotalAmount { get; set; }

    public ICollection<OrderDetail> OrderDetails { get; set; }
}
