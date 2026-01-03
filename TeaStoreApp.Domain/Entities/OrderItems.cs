namespace TeaStoreApp.Domain.Entities;

public class OrderItems
{
    public int OrderItemId { get; set; }
    public int OrderId { get; set; }
    public Orders Order { get; set; } = null!;
    
    public int ProductId { get; set; }
    public Products Product { get; set; } = null!;

    public decimal Price { get; set; }
    public int Qty { get; set; }
    public decimal Total { get; set; }
}