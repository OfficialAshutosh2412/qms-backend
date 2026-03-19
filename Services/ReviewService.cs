using Microsoft.AspNetCore.Http.HttpResults;
using QualityWebSystem.DTOs;
using QualityWebSystem.Models;
using QualityWebSystem.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace QualityWebSystem.Services
{
    public class ReviewService : IReviewService
    {
        //dependency variables
        private readonly IReviewRepo _repo;

        //dependency injection
        public ReviewService(IReviewRepo repo)
        {
            _repo = repo;
        }
        //create review service
        public async Task AddReviewAsync(CreateReviewDTO dto, string customerId)
        {
            var review = new Review
            {
                Rating = dto.Rating,
                Description = dto.Description,
                DeptId = dto.DeptId,
                CustomerId = customerId,
                //system defined
                CreatedDate = DateTime.UtcNow,
                IsEdited = false
            };

            await _repo.AddAsync(review);
        }

        //fetch customer review service
        public async Task<List<ReviewListDTO>> GetCustomerReviewsAsync(string customerId)
        {
            var reviewList = await _repo.GetReviewListAsync(customerId);

            return reviewList.Select(item => new ReviewListDTO
            {
                ReviewId = item.ReviewId,
                CustomerId = item.CustomerId,
                DeptId = item.DeptId,
                Rating = item.Rating,
                Description = item.Description,
                CreatedDate = item.CreatedDate,
                IsEdited = item.IsEdited,
                DeptName=item.Department.DeptName,
                isReplied=item.isReplied

            }).ToList();
        }
    
        //update customer review service
        public async Task<bool> UpdateReviewAsync(int id, EditReviewDTO dto, string userId)
        {
            return await _repo.UpdateAsync(id, dto, userId);
        }

        //delete customer review(optional)
        //public async Task<bool> DeleteCustomerReviewAsync(int id, string userId)
        //{
        //    return await _repo.DeleteAsync(id, userId);
        //}
    }
}
