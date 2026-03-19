using QualityWebSystem.Models;

namespace QualityWebSystem.Services
{
    public interface ITokenService
    {
        Task<string> GenerateJWTToken(AppUser user);
    }
}
