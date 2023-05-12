namespace TaskApp.WebApi.Abstraction;

public abstract class Entity
{
    protected Entity()
    {
        Id = Guid.NewGuid().ToString();
    }
    public string Id { get; private set; } 
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set;}
}
