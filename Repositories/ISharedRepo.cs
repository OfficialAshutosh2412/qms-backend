using QualityWebSystem.DTOs;
using QualityWebSystem.Models;

namespace QualityWebSystem.Repositories
{
    public interface ISharedRepo
    {
        Task<List<Department>> GetDeptAsync();
        Task<Review> GetReviewAsync(int id);
        Task<ReviewReply> GetReplyAsync(int id);
        Task<AppUser> GetProfileAsync(string id);
    }
}
