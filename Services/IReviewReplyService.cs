using QualityWebSystem.DTOs;
using QualityWebSystem.Models;

namespace QualityWebSystem.Services
{
    public interface IReviewReplyService
    {
        Task<bool> PostReplyAsync(ReviewReplyDTO dto , string adminId); 
        Task<List<FetchReviewRepliesDTO>> FetchReplyReviewsAsync();
    }
}
