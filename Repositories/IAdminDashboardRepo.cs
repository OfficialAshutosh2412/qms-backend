using QualityWebSystem.DTOs;
using QualityWebSystem.Models;

namespace QualityWebSystem.Repositories
{
    public interface IAdminDashboardRepo
    {
        Task<int> getCountAsync();
        Task<List<DepartmentSummaryDTO>> GetSummaryAsync();
        Task<SentimentDTO> GetSentimentAsync();
    }
}
