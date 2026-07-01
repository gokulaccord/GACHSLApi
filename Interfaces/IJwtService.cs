using GACHSLApi.Entities;

namespace GACHSLApi.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        string GenerateRefreshToken();
    }
}