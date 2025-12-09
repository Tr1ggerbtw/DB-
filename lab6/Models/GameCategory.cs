namespace Steam.Models;

public class GameCategory
{
    public Guid GameId { get; set; }
    public Guid CategoryId { get; set; }

    public Game Game { get; set; } = null!;
    public Category Category { get; set; } = null!;
}