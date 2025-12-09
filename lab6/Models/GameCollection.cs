namespace Steam.Models;

public class GameCollection
{
    public Guid GameCollectionId { get; set; }
    public Guid UserLibraryId { get; set; }
    public string Name { get; set; } = null!;
    
    public UserLibrary UserLibrary { get; set; } = null!;
    public ICollection<LibraryCollection> LibraryCollections { get; set; } = new List<LibraryCollection>();
}