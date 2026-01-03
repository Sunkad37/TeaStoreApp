namespace TeaStoreApp.Domain.Entities;

public class Categories
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ICollection<Products>  Products { get; set; } = new List<Products>();
}