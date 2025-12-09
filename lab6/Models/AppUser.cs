namespace Steam.Models;

public class AppUser
{
    public Guid AppUserId { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;

    public UserInfo? UserInfo { get; set; }
    public UserLibrary? UserLibrary { get; set; }
	public ICollection<Review> Reviews { get; set; } = new List<Review>();
}