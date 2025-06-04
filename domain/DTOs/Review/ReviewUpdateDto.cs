namespace domain.DTOs.Review;
public record ReviewUpdateDto
{
    public Guid Id { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
}
