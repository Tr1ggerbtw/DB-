namespace Steam.Models;

public class Review
{
    public Guid ReviewId { get; set; }
    public Guid GameId { get; set; }
    public Guid AppUserId { get; set; }
    
    public double Rating { get; set; } 
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }

    public Game Game { get; set; } = null!;
    public AppUser AppUser { get; set; } = null!;
}