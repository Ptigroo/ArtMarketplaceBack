﻿namespace ArtMarketplace.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public virtual ICollection<Product> ArtisanProducts { get; set; } = [];
        public virtual ICollection<Product> BuyerProducts { get; set; } = [];
    }
}
