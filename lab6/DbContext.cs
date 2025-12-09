using Microsoft.EntityFrameworkCore;
using Steam.Models;
public class GameLibraryContext : DbContext
{
    public GameLibraryContext(DbContextOptions<GameLibraryContext> options) 
        : base(options)
    {
    }
    
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<UserInfo> UserInfos { get; set; }
    public DbSet<UserLibrary> UserLibraries { get; set; }
    public DbSet<GameCollection> GameCollections { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Achievement> Achievements { get; set; }
    public DbSet<LibraryCollection> LibraryCollections { get; set; }
    public DbSet<Progress> Progresses { get; set; }
    public DbSet<GameCategory> GameCategories { get; set; }
    public DbSet<UnlockedAchievement> UnlockedAchievements { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<AppUser>()
            .HasKey(x => x.AppUserId);
        modelBuilder.Entity<UserInfo>()
            .HasKey(x => x.AppUserId); 
        modelBuilder.Entity<UserLibrary>()
            .HasKey(x => x.UserLibraryId);
        modelBuilder.Entity<GameCollection>()
            .HasKey(x => x.GameCollectionId);
        modelBuilder.Entity<Game>()
            .HasKey(x => x.GameId);
        modelBuilder.Entity<Category>()
            .HasKey(x => x.CategoryId);
        modelBuilder.Entity<Achievement>()
            .HasKey(x => x.AchievementId);
        modelBuilder.Entity<Review>()
            .HasKey(x => x.ReviewId);
        
        modelBuilder.Entity<LibraryCollection>()
            .HasKey(x => new { x.GameCollectionId, x.GameId });
        modelBuilder.Entity<Progress>()
            .HasKey(x => new { x.UserLibraryId, x.GameId });
        modelBuilder.Entity<GameCategory>()
            .HasKey(x => new { x.GameId, x.CategoryId });
        modelBuilder.Entity<UnlockedAchievement>()
            .HasKey(x => new { x.UserLibraryId, x.AchievementId });

        modelBuilder.Entity<AppUser>()
            .HasIndex(x => x.Username)
            .IsUnique();

        modelBuilder.Entity<Game>()
            .HasIndex(x => x.Name)
            .IsUnique();

        modelBuilder.Entity<Game>()
            .ToTable(x => x.HasCheckConstraint("CK_Game_Price", "\"Price\" > 0"));
        
        modelBuilder.Entity<Review>()
            .Property(x => x.Rating)
                .IsRequired()
                .HasAnnotation("CheckConstraint", "Rating >= 0 AND Rating <= 5");
        
        modelBuilder.Entity<Review>()
            .Property(x => x.Comment).HasMaxLength(1000);
        
        modelBuilder.Entity<AppUser>()
            .Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(32);

        modelBuilder.Entity<AppUser>()
            .Property(x => x.Password)
            .IsRequired()
            .HasMaxLength(128);

        modelBuilder.Entity<UserInfo>()
            .Property(x => x.PhoneNumber).HasMaxLength(20);
        
        modelBuilder.Entity<UserInfo>()
            .Property(x => x.Email).HasMaxLength(255);

        modelBuilder.Entity<UserInfo>()
            .Property(x => x.Birthday);
        
        modelBuilder.Entity<UserLibrary>()
            .Property(x => x.AppUserId).IsRequired();
        
        modelBuilder.Entity<Game>()
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(64);

        modelBuilder.Entity<Game>()
            .Property(x => x.Price)
            .IsRequired()
            .HasColumnType("decimal(10, 2)");
                
        modelBuilder.Entity<Game>()
            .Property(x => x.Description).HasMaxLength(2000);

        modelBuilder.Entity<Game>()
            .Property(x => x.IsIndie)
            .IsRequired()
            .HasDefaultValue(false);
        
        modelBuilder.Entity<Category>()
            .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(32);
                
        modelBuilder.Entity<Category>()
            .Property(x => x.Description).HasMaxLength(500);
        
        modelBuilder.Entity<Achievement>()
            .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(64);
                
        modelBuilder.Entity<Achievement>()
            .Property(x => x.Goal).HasMaxLength(500);
        
        modelBuilder.Entity<GameCollection>()
            .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(32);
        
        modelBuilder.Entity<AppUser>()
            .HasOne(x => x.UserInfo)
            .WithOne(x => x.AppUser)
            .HasForeignKey<UserInfo>(x => x.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AppUser>()
            .HasOne(x => x.UserLibrary)
            .WithOne(x => x.AppUser)
            .HasForeignKey<UserLibrary>(x => x.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserLibrary>()
            .HasMany(x => x.GameCollections)
            .WithOne(x => x.UserLibrary)
            .HasForeignKey(x => x.UserLibraryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Game>()
            .HasMany(x => x.Achievements)
            .WithOne(x => x.Game)
            .HasForeignKey(x => x.GameId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<LibraryCollection>()
            .HasOne(x => x.GameCollection)
            .WithMany(x => x.LibraryCollections)
            .HasForeignKey(x => x.GameCollectionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<LibraryCollection>()
            .HasOne(x => x.Game)
            .WithMany(x => x.LibraryCollections)
            .HasForeignKey(x => x.GameId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Progress>()
            .HasOne(x => x.UserLibrary)
            .WithMany(x => x.Progresses)
            .HasForeignKey(x => x.UserLibraryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Progress>()
            .HasOne(x => x.Game)
            .WithMany(x => x.Progresses)
            .HasForeignKey(x => x.GameId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<GameCategory>()
            .HasOne(x => x.Game)
            .WithMany(x => x.GameCategories)
            .HasForeignKey(x => x.GameId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<GameCategory>()
            .HasOne(x => x.Category)
            .WithMany(x => x.GameCategories)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UnlockedAchievement>()
            .HasOne(x => x.UserLibrary)
            .WithMany(x => x.UnlockedAchievements)
            .HasForeignKey(x => x.UserLibraryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UnlockedAchievement>()
            .HasOne(x => x.Achievement)
            .WithMany(x => x.UnlockedAchievements)
            .HasForeignKey(x => x.AchievementId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Review>()
            .HasOne(x => x.Game)
            .WithMany(x => x.Reviews)
            .HasForeignKey(x => x.GameId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Review>()
            .HasOne(x => x.AppUser)
            .WithMany(x => x.Reviews)
            .HasForeignKey(x => x.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}