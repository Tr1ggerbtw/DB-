namespace Steam.Models;

public class UnlockedAchievement
{
    public Guid UserLibraryId { get; set; }
    public Guid AchievementId { get; set; }
    public DateTime? DataComplete { get; set; }

    public UserLibrary UserLibrary { get; set; } = null!;
    public Achievement Achievement { get; set; } = null!;
}