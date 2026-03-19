using QualityWebSystem.DTOs;
using QualityWebSystem.Models;
using QualityWebSystem.Repositories;

namespace QualityWebSystem.Services
{
    public class ReviewReplyService:IReviewReplyService
    {
        private readonly IReviewReplyRepo _reviewReplyRepo;

        public ReviewReplyService(IReviewReplyRepo reviewReplyRepo)
        {
            _reviewReplyRepo = reviewReplyRepo;
        }

        public async Task<bool> PostReplyAsync(ReviewReplyDTO dto, string adminId)
        {
            var targetedReview = await _reviewReplyRepo.GetReviewByIdAsync(dto.ReviewId);
            if (targetedReview == null)
                throw new KeyNotFoundException("Review does not exixts!");

            bool isReplyExists = await _reviewReplyRepo.ReplyExistsAsync(dto.ReviewId);

            if (!isReplyExists)
                throw new InvalidOperationException("Review already replied!");
            var newReply = new ReviewReply
            {
                ReviewId = dto.ReviewId,
                ReplyMessage = dto.ReplyMessage,
                ReplyDate=DateTime.UtcNow,
                AdminId =adminId
            };

            bool isSuccess = await _reviewReplyRepo.PostReviewReplyAsync(newReply);

            if (!isSuccess)
                return false;

            return true;
        }

        public async Task<List<FetchReviewRepliesDTO>> FetchReplyReviewsAsync()
        {
            var list = await _reviewReplyRepo.FetchReplies();
            return list.Select(d => new FetchReviewRepliesDTO
            {
                ReplyId=d.ReplyId,
                ReviewId=d.ReviewId,
                ReplyMessage=d.ReplyMessage,
                AdminName=d.Admin.FullName,
                ReplyDate=d.ReplyDate,
            }).ToList();
        }
    }
}
