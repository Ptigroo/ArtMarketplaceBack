using ArtMarketplace.Domain.Models;
using data.Repository;
namespace ArtMarketplace.Domain.Services;
public interface ICategoryService
{
    Task<List<Category>> GetAllCategories();
}
public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task<List<Category>> GetAllCategories()
    {
        return await categoryRepository.GetAllCategories();
    }
}
