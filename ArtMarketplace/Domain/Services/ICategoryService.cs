using ArtMarketplace.Data;
using ArtMarketplace.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtMarketplace.Domain.Services;
public interface ICategoryService
{
    Task<List<Category>> GetAllCategories();
}
public class CategoryService(ArtMarketplaceDbContext dbContext) : ICategoryService
{
    public async Task<List<Category>> GetAllCategories()
    {
        return await dbContext.Categories.ToListAsync();
    }
}
