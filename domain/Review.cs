namespace ArtMarketplace.Domain.Models;
public class Review
{
    public Guid Id { get; set; }
    public float Rating { get; set; }
    public string Comment { get; set; }
}
