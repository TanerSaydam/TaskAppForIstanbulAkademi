using TaskApp.WebApi.Abstraction;

namespace TaskApp.WebApi.Models;

public sealed class Product: Entity
{    
    public string Description { get; set; }
    public string Category { get; set; }
    public decimal Unit { get; set; } 
    public decimal UnitPrice { get; set; }
    public bool Status { get; set; }
}