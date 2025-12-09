namespace Steam.Models;

public class Achievement
{
    public Guid AchievementId { get; set; }
    public Guid GameId { get; set; }
    
    public string Name { get; set; } = null!;
    public string? Goal { get; set; }

    public Game Game { get; set; } = null!;
    public ICollection<UnlockedAchievement> UnlockedAchievements { get; set; } = new List<UnlockedAchievement>();
}