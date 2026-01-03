namespace TeaStoreApp.Domain.Entities;

public class UserPhotos
{
    public int PhotoId { get; set; }
    
    public int UserId { get; set; }
    public Users User { get; set; }
    
    public string ImageUrl { get; set; } = null!;
    public DateTime UploadedAt { get; set; } 
}