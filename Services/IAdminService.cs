using Microsoft.AspNetCore.Mvc;
using QualityWebSystem.DTOs;
using QualityWebSystem.Repositories;

namespace QualityWebSystem.Services
{
    public interface IAdminService
    {
        Task<List<GetAllReviewAdminDTO>> GetAllReviewsAsync();
        //Task<(bool isDeptIdExists,List<GetAllReviewAdminDTO> list)>GetFilteredReviewsByDepartmentAsync(int id);

        //Task<List<GetAllReviewAdminDTO>> GetFilteredReviewsByRateAsync(int rating);
        Task<List<GetAllReviewAdminDTO>> GetFilteredReviewServiceAsync(int? deptId, int? rating, string? sentiment);
        Task<List<UserInfoDTO>> GetUserInfoAsync();
    }
}
