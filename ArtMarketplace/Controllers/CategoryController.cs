using ArtMarketplace.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArtMarketplace.Controllers;
[Route("category")]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    [HttpGet()]
    public async Task<IActionResult> GetAllProducts()
    {
        return Ok(await categoryService.GetAllCategories());
    }
}
