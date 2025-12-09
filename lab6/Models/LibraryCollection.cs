namespace Steam.Models;

public class LibraryCollection
{
    public Guid GameCollectionId { get; set; }
    public Guid GameId { get; set; }

    public GameCollection GameCollection { get; set; } = null!;
    public Game Game { get; set; } = null!;
}