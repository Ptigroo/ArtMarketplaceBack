﻿using ArtMarketplace.Controllers.DTOs.Product;
using ArtMarketplace.Domain.Models;
using data.Repository;
namespace ArtMarketplace.Domain.Services;
public interface IProductService
{
    Task<Guid> AddProductAsync(ProductCreateDto dto, Guid userId);
    Task EditProductAsync(ProductCreateDto dto, Guid productId);
    Task<IEnumerable<ProductGetDto>> GetArtisanProductsAsync(string imageUrl, Guid userId);
    Task<ProductGetDto> GetById(Guid productId);
    Task<IEnumerable<ProductGetDto>> GetAllAvailableProductsAsync(string imagesUrl);
    Task<IEnumerable<ProductGetDto>> GetProductsToDeliver(string imagesUrl);
    Task AddToBasketProduct(Guid productId, Guid userId);
    Task<IEnumerable<ProductGetDto>> GetBoughtProduct(string imagesUrl, Guid userId);
    Task<IEnumerable<ProductGetDto>> GetBasket(string imagesUrl, Guid userId);
    Task BuyBasket(Guid userId);
    Task<DeliveryStatus> SetDeliveryStatus(Guid productId);
}
public class ProductService(IProductRepository productRepository) : IProductService
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
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            CategoryId = dto.CategoryId,
            ImageUrl = imageUrl,
            ArtisanId = userId
        };
        var productId = await productRepository.AddProductAsync(product);
        return productId;
    }
    public async Task EditProductAsync(ProductCreateDto dto, Guid productId)
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
        await productRepository.EditProductAsync(productId, dto.Title, dto.Description, dto.Price, dto.CategoryId, imageUrl);
    }
    public async Task AddToBasketProduct(Guid productId, Guid userId)
    {
        await productRepository.SetBuyer(productId, userId);
    }

    public async Task<IEnumerable<ProductGetDto>> GetAllAvailableProductsAsync(string imageUrl)
    {
        var availableProducts = await productRepository.GetAllAvailableProductsAsync();
        return availableProducts.Select(p => p.ConvertToDto(imageUrl));
    }

    public async Task<IEnumerable<ProductGetDto>> GetArtisanProductsAsync(string imageUrl, Guid userId)
    {
        var products = await productRepository.GetArtisanProductsAsync(userId);
        return products.Select(p => p.ConvertToDto(imageUrl));
    }

    public async Task<IEnumerable<ProductGetDto>> GetBoughtProduct(string imagesServerUrl, Guid userId)
    {
        var products = await productRepository.GetBoughtProduct(userId);
        return products.Select(p => p.ConvertToDto(imagesServerUrl));
    }
    public async Task<IEnumerable<ProductGetDto>> GetBasket(string imagesServerUrl, Guid userId)
    {
        var products = await productRepository.GetBasket(userId);
        return products.Select(p => p.ConvertToDto(imagesServerUrl));
    }
    
    public async Task<ProductGetDto> GetById(Guid productId)
    {
        var product = await productRepository.GetById(productId);
        return new ProductGetDto
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            Price = product.Price,
            Category = product.Category.Name,
            ImageUrl = product.ImageUrl,
            ReviewId = product.ReviewId
        };
    }

    public async Task BuyBasket(Guid userId)
    {
        var currentBasket = await productRepository.GetBasket(userId);
        foreach (var product in currentBasket)
        {
            await productRepository.BuyProduct(product);
        }
    }

    public async Task<DeliveryStatus> SetDeliveryStatus(Guid productId)
    {
        var product = await productRepository.GetById(productId);
        DeliveryStatus nextStatus = product.DeliveryStatus switch
        {
            DeliveryStatus.ToPickAtArtist => DeliveryStatus.PickedFromArtist,
            DeliveryStatus.PickedFromArtist => DeliveryStatus.WaitingForDeliveryOfficier,
            DeliveryStatus.WaitingForDeliveryOfficier => DeliveryStatus.InDelivery,
            DeliveryStatus.InDelivery => DeliveryStatus.Delivered
        };
        await productRepository.SetDeliveryStatus(nextStatus, product);
        return nextStatus;
    }

    public async Task<IEnumerable<ProductGetDto>> GetProductsToDeliver(string imagesUrl)
    {
        return (await productRepository.GetProductsToDeliver()).Select(product => product.ConvertToDto(imagesUrl));
    }
}
