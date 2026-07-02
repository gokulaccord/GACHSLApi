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
    }
}