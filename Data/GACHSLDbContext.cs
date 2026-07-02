using Microsoft.EntityFrameworkCore;
using GACHSLApi.Entities;

namespace GACHSLApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<PasswordResetOtp> PasswordResetOtps { get; set; }
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentCategory> DocumentCategories { get; set; }
        public DbSet<Member> Members { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DocumentCategory>().HasData(
                new DocumentCategory { CategoryId = 1, CategoryName = "Agreement", IsActive = true },
                new DocumentCategory { CategoryId = 2, CategoryName = "Meeting Minutes", IsActive = true },
                new DocumentCategory { CategoryId = 3, CategoryName = "Legal", IsActive = true },
                new DocumentCategory { CategoryId = 4, CategoryName = "Financial", IsActive = true },
                new DocumentCategory { CategoryId = 5, CategoryName = "Architect", IsActive = true },
                new DocumentCategory { CategoryId = 6, CategoryName = "Government Approval", IsActive = true },
                new DocumentCategory { CategoryId = 7, CategoryName = "General", IsActive = true }
            );
        }
    }
}