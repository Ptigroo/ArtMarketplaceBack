using ArtMarketplace.Controllers.DTOs.Product;
using ArtMarketplace.Domain.Services;
using domain.DTOs.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtMarketplace.Controllers;
[Route("product")]
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
        var userId = User.FindFirst("id")?.Value;
        if (userId == null) return Unauthorized();
        var serverUrl = $"{Request.Scheme}://{Request.Host}";
        return Ok(await productService.GetArtisanProductsAsync(serverUrl, Guid.Parse(userId)));
    }

    [HttpGet("{productId}")]
    public async Task<ActionResult<ProductGetDto>> GetById(Guid productId)
    {
        return Ok(await productService.GetById(productId));
    }
    [HttpGet("availables")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> GetAllAvailableProducts()
    {
        var serverUrl = $"{Request.Scheme}://{Request.Host}";
        return Ok(await productService.GetAllAvailableProductsAsync(serverUrl));
    }

    [HttpGet("bought")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> GetBoughtProduct()
    {
        var serverUrl = $"{Request.Scheme}://{Request.Host}";
        var userId = User.FindFirst("id")?.Value;
        if (userId == null) return Unauthorized();
        return Ok(await productService.GetBoughtProduct(serverUrl, Guid.Parse(userId)));
    }
    [HttpPatch("addtobasket/{productId}")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> AddToBasketProduct(Guid productId)
    {
        var userId = User.FindFirst("id")?.Value;
        if (userId == null) return Unauthorized();
        await productService.AddToBasketProduct(productId, Guid.Parse(userId));
        return Ok();
    }
    [HttpGet("basket")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> GetBasket()
    {
        var serverUrl = $"{Request.Scheme}://{Request.Host}";
        var userId = User.FindFirst("id")?.Value;
        if (userId == null) return Unauthorized();
        return Ok(await productService.GetBasket(serverUrl, Guid.Parse(userId)));
    }
    [HttpPatch("buybasket")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> BuyBasket()
    {
        var userId = User.FindFirst("id")?.Value;
        if (userId == null) return Unauthorized();
        await productService.BuyBasket(Guid.Parse(userId));
        return NoContent();
    }
    [HttpPatch("review")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> Review([FromBody] ProductReviewDto product)
    {
        await productService.Review(product);
        return NoContent();
    }
}
