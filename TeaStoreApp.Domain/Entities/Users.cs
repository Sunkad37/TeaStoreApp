namespace TeaStoreApp.Domain.Entities;

public class Users
{
    public int UserId { get; set; }
    public string Name { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Phone { get; init; } = null!;
    public string PasswordHash { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    public UserPhotos? Photo { get; set; }
    public ICollection<Orders> Orders { get; set; } = new List<Orders>();

    public ICollection<ShoppingCartItems> CartItems { get; set; } = new List<ShoppingCartItems>();
}