using GACHSLApi.Data;
using GACHSLApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace GACHSLApi.Repositories
{
    public class PasswordResetOtpRepository : IPasswordResetOtpRepository
    {
        private readonly AppDbContext _context;

        public PasswordResetOtpRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PasswordResetOtp otp)
        {
            await _context.PasswordResetOtps.AddAsync(otp);
        }

        public async Task<PasswordResetOtp?> GetValidOtpAsync(int userId, string otp)
        {
            return await _context.PasswordResetOtps
                .FirstOrDefaultAsync(x =>
                    x.UserId == userId &&
                    x.Otp == otp &&
                    !x.IsUsed &&
                    x.ExpiryOn > DateTime.UtcNow);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}