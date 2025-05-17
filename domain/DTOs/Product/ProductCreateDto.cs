using Microsoft.AspNetCore.Http;
namespace ArtMarketplace.Controllers.DTOs.Product;
public record ProductCreateDto
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    public IFormFile? Image { get; set; }
}
