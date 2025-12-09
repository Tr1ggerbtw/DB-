namespace Steam.Models;

public class Game
{
    public Guid GameId { get; set; }
    public decimal Price { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public bool IsIndie { get; set; }

    public ICollection<Achievement> Achievements { get; set; } = new List<Achievement>();
    public ICollection<GameCategory> GameCategories { get; set; } = new List<GameCategory>();
    public ICollection<LibraryCollection> LibraryCollections { get; set; } = new List<LibraryCollection>();
    public ICollection<Progress> Progresses { get; set; } = new List<Progress>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}