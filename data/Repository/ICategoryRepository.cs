
using ArtMarketplace.Data;
using ArtMarketplace.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;
public interface ICategoryRepository
{
    Task<List<Category>> GetAllCategories();
}
public class CategoryRepository(ArtMarketplaceDbContext dbContext) : ICategoryRepository
{
    public async Task<List<Category>> GetAllCategories()
    {
        return await dbContext.Categories.ToListAsync();
    }
}

