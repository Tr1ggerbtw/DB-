namespace Steam.Models;

public class Progress
{
    public Guid UserLibraryId { get; set; }
    public Guid GameId { get; set; }
    public int? HoursPlayed { get; set; }

    public UserLibrary UserLibrary { get; set; } = null!;
    public Game Game { get; set; } = null!;
}