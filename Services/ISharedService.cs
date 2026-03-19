using QualityWebSystem.DTOs;

namespace QualityWebSystem.Services
{
    public interface ISharedService
    {
        Task<List<DepartmentDTO>> GetDepartmentsAsync();
        Task<EditReviewDTO> GetReviewByIdAsync(int id);
        Task<FetchReviewRepliesDTO> GetReviewReplyByIdAsync(int id);
        Task<ProfileDTO> GetCustomerProfileAsync(string id);
    }
}
