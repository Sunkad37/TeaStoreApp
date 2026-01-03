namespace TeaStoreApp.Domain.Entities;

public class ShoppingCartItems
{
    public int CartItemId { get; set; }
    
    public int ProductId { get; set; }
    public Products Product { get; set; } = null!;
    
    public int UserId { get; set; }
    public Users User { get; set; } = null!;
    
    public decimal Price { get; set; }
    public int Qty { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
}