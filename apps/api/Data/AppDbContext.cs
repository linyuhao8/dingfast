using Microsoft.EntityFrameworkCore;
using Shared.Domain.Users.Models;
using Shared.Domain.Merchants.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Merchant> Merchants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 設定 User 的 Email 欄位為唯一
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // 設定 Guest 的 SessionId 欄位為唯一
        modelBuilder.Entity<Guest>()
            .HasIndex(g => g.SessionId)
            .IsUnique();

    // Merchant 與 User 的關聯
    modelBuilder.Entity<Merchant>()
        .HasOne(m => m.User)
        .WithMany()  // User 沒有 Merchants 集合，避免雙向導航
        .HasForeignKey(m => m.UserId)
        .OnDelete(DeleteBehavior.SetNull);

    }
}
