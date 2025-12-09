namespace Steam.Models;

public class Category
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = null!;
    // public string? Description { get; set; }
    public ICollection<GameCategory> GameCategories { get; set; } = new List<GameCategory>();
}