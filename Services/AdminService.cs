using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using QualityWebSystem.DTOs;
using QualityWebSystem.Repositories;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QualityWebSystem.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepo _repo;

        public AdminService(IAdminRepo repo)
        {
            _repo = repo;
        }

        public async Task<List<GetAllReviewAdminDTO>> GetAllReviewsAsync()
        {
            var list = await _repo.GetAsync();

            return list.Select(data => new GetAllReviewAdminDTO
            {
                ReviewId = data.ReviewId,
                Rating = data.Rating,
                Description = data.Description,
                CreatedDate = data.CreatedDate,
                IsEdited = data.IsEdited,
                CustomerName = data.Customer.FullName,
                CustomerEmail = data.Customer.Email,
                DeptName = data.Department.DeptName,
                isReplied=data.isReplied,
                
            }).ToList();
        }
        public async Task<List<GetAllReviewAdminDTO>> GetFilteredReviewServiceAsync(int? deptId, int? rating, string? sentiment)
        {
            var list = await _repo.GetFilteredReviewAsync(deptId, rating, sentiment);
             return list.Select(data => new GetAllReviewAdminDTO
            {
                ReviewId = data.ReviewId,
                Rating = data.Rating,
                Description = data.Description,
                CreatedDate = data.CreatedDate,
                IsEdited = data.IsEdited,
                CustomerName = data.Customer.FullName,
                CustomerEmail = data.Customer.Email,
                DeptName = data.Department.DeptName,
            }).ToList();
        }

        public async Task<List<UserInfoDTO>> GetUserInfoAsync()
        {
            var list = await _repo.FetchUserInfo();
            return list.Select(m => new UserInfoDTO
            {
                UserId=m.Id,
                Username=m.UserName,
                FullName=m.FullName,
                Email=m.Email,
                CreatedAt=m.CreatedAt
            }).ToList();
        }
    }
}
