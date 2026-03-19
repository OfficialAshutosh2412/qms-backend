using QualityWebSystem.DTOs;
using QualityWebSystem.Repositories;

namespace QualityWebSystem.Services
{
    public class AdminDashboardService:IAdminDashboardService
    {
        private readonly IAdminDashboardRepo _repo;

        public AdminDashboardService(IAdminDashboardRepo repo)
        {
            _repo = repo;
        }

        public async Task<int> GetReviewsCountAsync()
        {
            return await _repo.getCountAsync();
        }
        public async Task<List<DepartmentSummaryDTO>> GetDepartmentSummaryAsync()
        {
            return await _repo.GetSummaryAsync();
        }

        public async Task<SentimentDTO> GetSentimentDataAsync()
        {
            return await _repo.GetSentimentAsync();
        }
    }
}
