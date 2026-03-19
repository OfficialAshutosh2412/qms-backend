using Microsoft.EntityFrameworkCore;
using QualityWebSystem.Data;
using QualityWebSystem.DTOs;
using QualityWebSystem.Models;

namespace QualityWebSystem.Repositories
{
    public class AdminDashboardRepo:IAdminDashboardRepo
    {
        private readonly AppDbContext _context;

        public AdminDashboardRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> getCountAsync()
        {
            return await _context.Reviews.CountAsync();
        }
        public async Task<List<DepartmentSummaryDTO>> GetSummaryAsync()
        {
            var data = await (
                    from dept in _context.Departments
                    join rev in _context.Reviews
                    on dept.DeptId equals rev.DeptId into jointData

                    from jd in jointData.DefaultIfEmpty()

                    group jd by dept.DeptName into groupedData

                    select new DepartmentSummaryDTO
                    {
                        Department = groupedData.Key,
                        TotalReviews = groupedData.Count(row => row != null),
                        PositiveReviews = groupedData.Count(row => row != null && row.Rating >= 4),
                        NeutralReviews = groupedData.Count(row => row != null && row.Rating == 3),
                        NegativeReviews = groupedData.Count(row => row != null && row.Rating <= 2)
                    }
                ).ToListAsync();
            return data;
        }
        public async Task<SentimentDTO> GetSentimentAsync()
        {
            var result = await _context.Reviews
                .GroupBy(x => 1)
                .Select(data => new SentimentDTO
                {
                    Positive = data.Count(x => x.Rating >= 4),
                    Neutral = data.Count(x => x.Rating == 3),
                    Negative = data.Count(x => x.Rating <= 2),
                }).FirstOrDefaultAsync();
            return result ?? new SentimentDTO
            {
                Positive = 0,
                Neutral = 0,
                Negative = 0
            };
        }
    }
}
