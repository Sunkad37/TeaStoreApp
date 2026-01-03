namespace TeaStoreApp.Domain.Entities;

public class Products
{
    public int ProductId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }

    public int CategoryId { get; set; }
    public Categories Category { get; set; } = null!;

    public bool IsTrending { get; set; }
    public bool IsBestSelling { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<ShoppingCartItems> CartItems { get; set; } = new List<ShoppingCartItems>();

    public ICollection<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
}