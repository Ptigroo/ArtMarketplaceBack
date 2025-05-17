using ArtMarketplace.Controllers.DTOs.Product;
using ArtMarketplace.Data;
using ArtMarketplace.Domain.Models;
using Microsoft.EntityFrameworkCore;
namespace ArtMarketplace.Domain.Services;
public interface IProductService
{
    Task<Guid> AddProductAsync(ProductCreateDto dto, Guid userId);
    Task<IEnumerable<ProductGetDto>> GetArtisanProductsAsync(Guid userId);
    Task<ProductGetDto> GetById(Guid productId);
    Task<List<Product>> GetAllAsync(string imagesUrl);
    Task BuyProductAsync(Guid productId, Guid userId);
}
public class ProductService(ArtMarketplaceDbContext dbContext) : IProductService
{
    public async Task<Guid> AddProductAsync(ProductCreateDto dto, Guid userId)
    {

        string? imageUrl = null;

        if (dto.Image != null && dto.Image.Length > 0)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Image.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await dto.Image.CopyToAsync(fileStream);
            }

            imageUrl = $"/uploads/{uniqueFileName}";
        }
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            CategoryId = dto.CategoryId,
            ImageUrl = imageUrl,
            ArtisanId = userId
        };

        dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();
        return product.Id;
    }

    public async Task BuyProductAsync(Guid productId, Guid userId)
    {
        var product = await dbContext.Products.FirstAsync(product => product.Id == productId);
        product.BuyerId = userId;
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<Product>> GetAllAsync(string imageUrl)
    {
        
        return await dbContext.Products
        .Select(p => new Product {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            Price = p.Price,
            Category = p.Category,
            ImageUrl = $"{imageUrl}{p.ImageUrl}"
        })
        .ToListAsync();
    }

    public async Task<IEnumerable<ProductGetDto>> GetArtisanProductsAsync(Guid userId)
    {

        var products = await dbContext.Products
            .Where(p => p.ArtisanId == userId)
            .Select(p => new ProductGetDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Price = p.Price,
                Category = p.Category.Name,
                ImageUrl = p.ImageUrl
            })
            .ToListAsync();
        return products;
    }

    public async Task<ProductGetDto> GetById(Guid productId)
    {
        var product = await dbContext.Products.FindAsync(productId);
        return new ProductGetDto
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            Price = product.Price,
            Category = product.Category.Name,
            ImageUrl = product.ImageUrl
        };
    }

}
