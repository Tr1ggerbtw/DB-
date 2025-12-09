namespace Steam.Models;

public class UserLibrary
{
    public Guid UserLibraryId { get; set; }
    public Guid AppUserId { get; set; }

    public AppUser AppUser { get; set; } = null!;
    public ICollection<GameCollection> GameCollections { get; set; } = new List<GameCollection>();
    public ICollection<Progress> Progresses { get; set; } = new List<Progress>();
    public ICollection<UnlockedAchievement> UnlockedAchievements { get; set; } = new List<UnlockedAchievement>();
}