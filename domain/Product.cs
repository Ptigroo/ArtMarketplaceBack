using ArtMarketplace.Controllers.DTOs.Product;

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
    public ProductStatus ProductStatus { get; set; } = ProductStatus.Available;
    public DeliveryStatus DeliveryStatus { get; set; } = DeliveryStatus.ToPickAtArtist;
    public ProductGetDto ConvertToDto(string imageServerUrl)
    {
        return new ProductGetDto
        {
            Id = Id,
            Title = Title,
            Description = Description,
            Price = Price,
            Category = Category.Name,
            ImageUrl = $"{imageServerUrl}{ImageUrl}",
            ProductStatus = ProductStatus.ToString(),
            DeliveryStatus = DeliveryStatus.ToString()
        };
    }
}
public enum ProductStatus { Available, Basket, Bought }
public enum DeliveryStatus { ToPickAtArtist, PickedFromArtist, WaitingForDeliveryOfficier, InDelivery, Delivered }