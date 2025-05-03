namespace ArtMarketplace.Controllers.DTOs
{
    public record RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
