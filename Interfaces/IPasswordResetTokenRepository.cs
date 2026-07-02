using GACHSLApi.Entities;

namespace GACHSLApi.Interfaces
{
    public interface IPasswordResetTokenRepository
    {
        Task AddAsync(PasswordResetToken token);

        Task<PasswordResetToken?> GetValidTokenAsync(string token);

        Task UpdateAsync(PasswordResetToken token);

        Task SaveChangesAsync();
    }
}