using Microsoft.EntityFrameworkCore;
using QualityWebSystem.Data;
using QualityWebSystem.DTOs;
using QualityWebSystem.Models;

namespace QualityWebSystem.Repositories
{
    public class ReviewReplyRepo : IReviewReplyRepo
    {
        private readonly AppDbContext _context;

        public ReviewReplyRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {
            return await _context.Reviews.FirstOrDefaultAsync(data => data.ReviewId == id);
        }
        public async Task<bool> ReplyExistsAsync(int id)
        {
            var result = await _context.ReviewReplies.FirstOrDefaultAsync(data => data.ReviewId == id);
            if (result != null)
                return false;

            return true;
        }
        public async Task<bool> PostReviewReplyAsync(ReviewReply reply)
        {
            //posting reply
            _context.ReviewReplies.Add(reply);

            //update isReplied true on reviews table
            var result =await _context.Reviews.FindAsync(reply.ReviewId);

            if (result != null)
            {
                result.isReplied = true;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ReviewReply>> FetchReplies()
        {
            return await _context.ReviewReplies
                .AsNoTracking()
                .Include(data => data.Admin)
                .ToListAsync();
        }

    }
}
