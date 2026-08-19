using Microsoft.EntityFrameworkCore;
using socialplatform.Models;

namespace socialplatform.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notification> Notifications { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
          .HasOne(c => c.User)
          .WithMany()
          .HasForeignKey(c => c.UserID)
          .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.User)
                .WithMany()
                .HasForeignKey(l => l.UserID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Follow>()
    .HasOne(f => f.Follower)
    .WithMany()
    .HasForeignKey(f => f.FollowerID)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Follow>()
                .HasOne(f => f.Following)
                .WithMany()
                .HasForeignKey(f => f.FollowingID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Message>()
    .HasOne(m => m.Gonderen)
    .WithMany()
    .HasForeignKey(m => m.GonderenId)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Alici)
                .WithMany()
                .HasForeignKey(m => m.AliciID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Notification>()
    .HasOne(n => n.User)
    .WithMany()
    .HasForeignKey(n => n.UserID)
    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}