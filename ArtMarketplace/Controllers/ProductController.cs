using ArtMarketplace.Controllers.DTOs.Product;
using ArtMarketplace.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ArtMarketplace.Controllers;

public class ProductController(IProductService productService) : ControllerBase
{
    [HttpPost("addproduct")]
    [RequestSizeLimit(10_000_000)]
    [Authorize(Roles = "Artisan")]
    public async Task<IActionResult> Add([FromForm] ProductCreateDto dto)
    {
        var userId = User.FindFirst("id")?.Value;
        if (userId == null) return Unauthorized();
        var addedProductId = await productService.AddProductAsync(dto, Guid.Parse(userId));
        return CreatedAtAction(nameof(Add), new { id = addedProductId });
    }
    [HttpGet("artisan")]
    public async Task<ActionResult<IEnumerable<ProductGetDto>>> GetArtisanProducts()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();
        return Ok(await productService.GetArtisanProductsAsync(Guid.Parse(userId)));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductGetDto>> GetById(Guid productId)
    {
        return Ok(await productService.GetById(productId));
    }
    [HttpGet("all")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> GetAllProducts()
    {
        var serverUrl = $"{Request.Scheme}://{Request.Host}";
        return Ok(await productService.GetAllAsync(serverUrl));
    }
    [HttpPatch("buy")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> BuyProduct(Guid productId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();
        await productService.BuyProductAsync(productId, Guid.Parse(userId));
        return Ok();
    }
}
