namespace Steam.Models;

public class UserInfo
{
    public Guid AppUserId { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public DateTime Birthday { get; set; }

    public AppUser AppUser { get; set; } = null!;
}