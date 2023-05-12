using System.ComponentModel.DataAnnotations.Schema;
using TaskApp.WebApi.Abstraction;

namespace TaskApp.WebApi.Models
{
    public sealed class OrderDetail : Entity
    {
        [ForeignKey("Order")]
        public string OrderId { get; set; }
        public Order Order { get; set; }

        [ForeignKey("Product")]
        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}
