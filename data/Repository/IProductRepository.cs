using ArtMarketplace.Data;
using ArtMarketplace.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;
public interface IProductRepository
{
    Task<Guid> AddProductAsync(Product product);
    Task EditProductAsync(Guid productId, string title, string description, decimal price, Guid categoryId, string? imageUrl);
    Task<List<Product>> GetArtisanProductsAsync(Guid userId);
    Task<Product> GetById(Guid productId);
    Task<string?> GetImage(Guid productId);
    Task<List<Product>> GetAllAvailableProductsAsync();
    Task SetBuyer(Guid productId, Guid userId);
    Task<List<Product>> GetBoughtProduct(Guid userId);
    Task<List<Product>> GetBasket(Guid userId);
    Task BuyProduct(Product product);
    Task SetDeliveryStatus(DeliveryStatus deliveryStatus, Product product);
    Task<IEnumerable<Product>> GetProductsToDeliver();
}
public class ProductRepository(ArtMarketplaceDbContext dbContext) : IProductRepository
{
    public async Task<Guid> AddProductAsync(Product product)
    {
        Guid productId = dbContext.Products.Add(product).Entity.Id;
        await dbContext.SaveChangesAsync();
        return productId;
    }
    public async Task<string?> GetImage(Guid productId)
    {
        return (await dbContext.Products.FirstOrDefaultAsync(product => product.Id == productId))?.ImageUrl ?? throw new KeyNotFoundException("");
    }
    public async Task EditProductAsync(Guid productId, string title, string description, decimal price, Guid categoryId, string? imageUrl)
    {
        var productToEdit = await dbContext.Products.Include(prod => prod.Category).FirstAsync(prod => productId == prod.Id);
        if (imageUrl != null)
        {
            productToEdit.ImageUrl = imageUrl;
        }
        productToEdit.Title = title;
        productToEdit.Description = description;
        productToEdit.Price = price;
        productToEdit.CategoryId = categoryId;

        dbContext.Products.Update(productToEdit);
        await dbContext.SaveChangesAsync();
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
        return await dbContext.Products.Include(product => product.Category).Include(product => product.Review)
        .Where(p => p.BuyerId == null)
        .ToListAsync();
    }

    public async Task<List<Product>> GetArtisanProductsAsync(Guid userId)
    {
        return await dbContext.Products.Include(product => product.Category).Include(product => product.Review)
            .Where(p => p.ArtisanId == userId)
            .ToListAsync();
    }

    public async Task<List<Product>> GetBoughtProduct(Guid userId)
    {
        return await dbContext.Products.Include(product => product.Category).Include(product => product.Review)
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
        var product = await dbContext.Products.Include(product => product.Category).Include(product => product.Review).FirstOrDefaultAsync(product => product.Id == productId) ?? throw new Exception($"Product with id: {productId} does not exist");
        return product;
    }

    public async Task SetDeliveryStatus(DeliveryStatus deliveryStatus, Product product)
    {
        product.DeliveryStatus = deliveryStatus;
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsToDeliver()
    {
        return await dbContext.Products.Include(product => product.Category).Where(product => product.ProductStatus == ProductStatus.Bought && product.DeliveryStatus != DeliveryStatus.Delivered).ToListAsync();
    }
}
