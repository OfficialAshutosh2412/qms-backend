using QualityWebSystem.DTOs;
using QualityWebSystem.Models;

namespace QualityWebSystem.Repositories
{
    public interface IReviewReplyRepo
    {
        Task<Review> GetReviewByIdAsync(int id);
        Task<bool> ReplyExistsAsync(int id);
        Task<bool> PostReviewReplyAsync(ReviewReply reply);
        Task<List<ReviewReply>> FetchReplies();
    }
}
