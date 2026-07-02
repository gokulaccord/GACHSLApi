using GACHSLApi.Entities;

public interface IPasswordResetOtpRepository
{
    Task AddAsync(PasswordResetOtp otp);

    Task<PasswordResetOtp?> GetValidOtpAsync(int userId, string otp);

    Task SaveChangesAsync();
}