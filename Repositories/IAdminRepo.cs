using QualityWebSystem.DTOs;
using QualityWebSystem.Models;

namespace QualityWebSystem.Repositories
{
    public interface IAdminRepo
    {
        Task<List<Review>> GetAsync();
        Task<List<Review>> GetFilteredReviewAsync(int? deptId, int? rating, string? sentiment);
        Task<List<AppUser>> FetchUserInfo();
    }
}
