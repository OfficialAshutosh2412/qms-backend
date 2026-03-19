using QualityWebSystem.DTOs;
using QualityWebSystem.Models;

namespace QualityWebSystem.Services
{
    public interface IReviewService
    {
        Task AddReviewAsync(CreateReviewDTO dto, string customerId);
        Task<List<ReviewListDTO>> GetCustomerReviewsAsync(string customerId);
        Task<bool> UpdateReviewAsync(int id, EditReviewDTO dto, string userId);
        //optional api delete
        //Task<bool> DeleteCustomerReviewAsync(int id, string userId);
    }
}
