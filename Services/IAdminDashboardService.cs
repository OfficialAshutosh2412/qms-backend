using QualityWebSystem.DTOs;

namespace QualityWebSystem.Services
{
    public interface IAdminDashboardService
    {
        Task<int> GetReviewsCountAsync();
        Task<List<DepartmentSummaryDTO>> GetDepartmentSummaryAsync();
        Task<SentimentDTO> GetSentimentDataAsync();
    }
}
