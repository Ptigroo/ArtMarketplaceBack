using ArtMarketplace.Data;
using ArtMarketplace.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace data;
public static class DataSeeder
{
    public static void SeedData(ArtMarketplaceDbContext context, IPasswordHasher<User> passwordHasher)
    {
        context.Database.Migrate();
        if (!context.Categories.Any())
        {
            var categoryPainture = new Category { Id = Guid.NewGuid(), Name = "Painture" };
            var categorySculpture= new Category { Id = Guid.NewGuid(), Name = "Sculpture" };
            var categoryPhoto = new Category { Id = Guid.NewGuid(), Name = "Photo" };
            context.Categories.AddRange(
                categoryPainture,
                categorySculpture,
                categoryPhoto
            );
            var artisanUser = new User
            {
                Id = Guid.NewGuid(),
                Email = "artisan@projet-web.com",
                Role = "Artisan",
            };
            artisanUser.PasswordHash = passwordHasher.HashPassword(artisanUser, "artisan");
            var customerUser = new User
            {
                Id = Guid.NewGuid(),
                Email = "customer@projet-web.com",
                Role = "Customer",
            };
            customerUser.PasswordHash = passwordHasher.HashPassword(customerUser, "customer");
            var deliveryUser = new User
            {
                Id = Guid.NewGuid(),
                Email = "delivery@projet-web.com",
                Role = "DeliveryPartner",
            };
            deliveryUser.PasswordHash = passwordHasher.HashPassword(deliveryUser, "delivery");
            context.Users.Add(deliveryUser);
            context.Users.Add(artisanUser);
            context.Users.Add(customerUser);

            var produit1 = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Singe fluo",
                Description = "Un singe en plastique sur lequel mon fils a vider son pot de magicolore",
                Price = 100,
                CategoryId = categorySculpture.Id,
                ArtisanId = artisanUser.Id,
                ImageUrl = "/uploads/38780b72-ba89-4c17-90f7-ccac15331606.jpg",
            };
            var produit1achete = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Produit acheté",
                Description = "Un singe en plastique sur lequel mon fils a vider son pot de magicolore",
                Price = 100,
                CategoryId = categorySculpture.Id,
                ArtisanId = artisanUser.Id,
                ImageUrl = "/uploads/38780b72-ba89-4c17-90f7-ccac15331606.jpg",
                BuyerId = customerUser.Id,
                ProductStatus = ProductStatus.Bought,
                DeliveryStatus = DeliveryStatus.ToPickAtArtist
            };

            var produit2 = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Painture bleu et rouge",
                Description = "Reconstitution d'un mur devant lequel quelqu'un s'est fait fusiller (glauque mais si ca se vend)",
                Price = 100,
                CategoryId = categoryPainture.Id,
                ArtisanId = artisanUser.Id,
                ImageUrl = "/uploads/543feb8b-c7ab-4a4e-b581-2c63e1b946d7.jpg",
            };
            var produit2reserve = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Produit réservé",
                Description = "Reconstitution d'un mur devant lequel quelqu'un s'est fait fusiller (glauque mais si ca se vend)",
                Price = 100,
                CategoryId = categoryPainture.Id,
                ArtisanId = artisanUser.Id,
                ImageUrl = "/uploads/543feb8b-c7ab-4a4e-b581-2c63e1b946d7.jpg",
                BuyerId = customerUser.Id,
                ProductStatus = ProductStatus.Basket,
            };
            var produit3 = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Buste moderne",
                Description = "Un buste de quelqu'un qui a fait des choses",
                Price = 100,
                CategoryId = categorySculpture.Id,
                ArtisanId = artisanUser.Id,
                ImageUrl = "/uploads/9ad4b33b-a74b-49d1-85a4-fcf2f7e819a0.jpg",
            };
            var produit4 = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Main",
                Description = "Il a la la réponse mais n'ose pas tout à fait prendre la parole",
                Price = 100,
                CategoryId = categorySculpture.Id,
                ArtisanId = artisanUser.Id,
                ImageUrl = "/uploads/bbb3dbd5-5233-43ea-93ad-443e0d65b235.jpg",
            };
            var produit5 = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Magritte",
                Description = "Ketchup renversé sur un vrai Magritte",
                Price = 100,
                CategoryId = categoryPainture.Id,
                ArtisanId = artisanUser.Id,
                ImageUrl = "/uploads/e0f5d7c3-b88b-43e0-9460-c8602507cdd2.jpg",
            };
            var produit6 = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Diplodocus",
                Description = "Je ne savais pas si je le mettais en sculpture ou painture",
                Price = 100,
                CategoryId = categoryPhoto.Id,
                ArtisanId = artisanUser.Id,
                ImageUrl = "/uploads/faee8023-b676-42f5-8dc1-4ed5064aac1d.jpg",
            };

            context.Products.Add(produit1achete);
            context.Products.Add(produit2reserve);
            context.Products.Add(produit1);
            context.Products.Add(produit2);
            context.Products.Add(produit3);
            context.Products.Add(produit4);
            context.Products.Add(produit5);
            context.Products.Add(produit6);
            context.SaveChanges();
        }
    }

}
