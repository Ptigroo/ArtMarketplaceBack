namespace domain.DTOs.Product;
public record ProductReviewDto
{
    public Guid Id { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
}
