namespace ArtMarketplace.Domain.Models;
public class Review
{
    public Guid Id { get; set; }
    public float Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public string Response { get; set; } = string.Empty;
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
}
