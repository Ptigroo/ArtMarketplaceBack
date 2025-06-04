namespace domain.DTOs.Review;
public record ReviewCreateDto
{
    public Guid ProductId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }

}
