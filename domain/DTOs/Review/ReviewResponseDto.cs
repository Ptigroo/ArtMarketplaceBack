namespace domain.DTOs.Review;
public record ReviewResponseDto
{
    public Guid Id { get; set; }
    public string ResponseComment { get; set; }
}
