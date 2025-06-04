namespace ArtMarketplace.Controllers.DTOs.Product;
public record ProductGetDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public string? ImageUrl { get; set; }
    public string ProductStatus { get; set; }
    public string DeliveryStatus { get; set; }
    public Guid? ReviewId { get; set; }
}
