using GACHSLApi.Entities;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken refreshToken);

    Task<RefreshToken?> GetByTokenAsync(string token);

    Task<RefreshToken?> GetValidRefreshTokenAsync(string token);

    Task UpdateAsync(RefreshToken refreshToken);

    Task SaveChangesAsync();
}