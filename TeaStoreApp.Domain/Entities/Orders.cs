namespace TeaStoreApp.Domain.Entities;

public class Orders
{
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public Users User { get; set; } = null!;
    
    public string Address { get; set; } = null!;
    public decimal OrderTotal { get; set; }
    public DateTime OrderDate { get; set; }
    public string? OrderStatus { get; set; }

    public ICollection<OrderItems> OrderItems { get; set; } =  new List<OrderItems>();
}