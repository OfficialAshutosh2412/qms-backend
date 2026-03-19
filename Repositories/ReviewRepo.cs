using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QualityWebSystem.Data;
using QualityWebSystem.DTOs;
using QualityWebSystem.Models;
using System.Runtime.InteropServices;

namespace QualityWebSystem.Repositories
{
    public class ReviewRepo : IReviewRepo
    {
        private readonly AppDbContext _context;

        public ReviewRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Review>> GetReviewListAsync(string customerId)
        {
            return await _context.Reviews
                .Include(m => m.Department)
                .Where(r => r.CustomerId == customerId)
                .OrderByDescending(r => r.CreatedDate)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(int id, EditReviewDTO dto, string userId)
        {
            var targetedReview = await _context.Reviews.FirstOrDefaultAsync(model => model.ReviewId == id && model.CustomerId == userId);

            if (targetedReview == null)
                return false;

            targetedReview.Rating = dto.Rating;
            targetedReview.Description = dto.Description;
            targetedReview.DeptId = dto.DeptId;
            targetedReview.IsEdited = true;

            await _context.SaveChangesAsync();
            return true;
        }
        //optional
        //public async Task<bool> DeleteAsync(int id, string userId)
        //{
        //    var targetedReview = await _context.Reviews.FirstOrDefaultAsync(model => model.ReviewId == id && model.CustomerId == userId);

        //    if (targetedReview == null)
        //        return false;

        //    _context.Reviews.Remove(targetedReview);

        //    await _context.SaveChangesAsync();
        //    return true;
        //}
    }
}
