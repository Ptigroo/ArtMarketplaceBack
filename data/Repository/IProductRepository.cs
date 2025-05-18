using ArtMarketplace.Controllers.DTOs.Product;
using ArtMarketplace.Data;
using ArtMarketplace.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;
public interface IProductRepository
{
    Task<Guid> AddProductAsync(Product product);
    Task<List<Product>> GetArtisanProductsAsync(Guid userId);
    Task<Product> GetById(Guid productId);
    Task<List<Product>> GetAllAvailableProductsAsync();
    Task SetBuyer(Guid productId, Guid userId);
    Task<List<Product>> GetBoughtProduct(Guid userId);
    Task<List<Product>> GetBasket(Guid userId);
    Task BuyProduct(Product product);
}
public class ProductRepository(ArtMarketplaceDbContext dbContext) : IProductRepository
{
    public async Task<Guid> AddProductAsync(Product product)
    {
        Guid productId =  dbContext.Products.Add(product).Entity.Id;
        await dbContext.SaveChangesAsync();
        return productId;
    }

    public async Task SetBuyer(Guid productId, Guid userId)
    {
        var product = await GetById(productId);
        product.BuyerId = userId;
        product.ProductStatus = ProductStatus.Basket;
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<Product>> GetAllAvailableProductsAsync()
    {
        return await dbContext.Products.Include(product => product.Category)
        .Where(p => p.BuyerId == null)
        .ToListAsync();
    }

    public async Task<List<Product>> GetArtisanProductsAsync(Guid userId)
    {
        return await dbContext.Products.Include(product => product.Category)
            .Where(p => p.ArtisanId == userId)
            .ToListAsync();
    }

    public async Task<List<Product>> GetBoughtProduct(Guid userId)
    {
        return await dbContext.Products.Include(product => product.Category)
        .Where(p => p.BuyerId == userId && p.ProductStatus == ProductStatus.Bought)
        .ToListAsync();
    }
    public async Task<List<Product>> GetBasket(Guid userId)
    {
        return await dbContext.Products.Include(product => product.Category)
        .Where(p => p.BuyerId == userId && p.ProductStatus == ProductStatus.Basket)
        .ToListAsync();
    }
    public async Task BuyProduct(Product product)
    {
        product.ProductStatus = ProductStatus.Bought;
        await dbContext.SaveChangesAsync();
    }

    public async Task<Product> GetById(Guid productId)
    {
        var product = await dbContext.Products.Include(product => product.Category).FirstOrDefaultAsync(product => product.Id == productId) ?? throw new Exception($"Product with id: {productId} does not exist");
        return product;
    }
}
