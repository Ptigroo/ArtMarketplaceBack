namespace ArtMarketplace.Domain.Models;
public class Product
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public string? ImageUrl { get; set; }
    public Guid ArtisanId { get; set; }
    public User Artisan { get; set; }
    public Guid? BuyerId { get; set; }
    public User Buyer { get; set; }
}
