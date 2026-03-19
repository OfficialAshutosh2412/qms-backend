using QualityWebSystem.DTOs;
using QualityWebSystem.Models;

namespace QualityWebSystem.Repositories
{
    public interface IReviewRepo
    {
        Task AddAsync(Review review);
        Task<List<Review>> GetReviewListAsync(string customerId);
        Task<bool> UpdateAsync(int id, EditReviewDTO dto, string userId);
        //optional task
        //Task<bool> DeleteAsync(int id, string userId);
    }
}
