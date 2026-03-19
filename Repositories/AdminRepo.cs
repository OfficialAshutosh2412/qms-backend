using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QualityWebSystem.Data;
using QualityWebSystem.DTOs;
using QualityWebSystem.Models;

namespace QualityWebSystem.Repositories
{
    public class AdminRepo : IAdminRepo
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        

        public AdminRepo(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //get all reviews
        public async Task<List<Review>> GetAsync()
        {
            return await _context.Reviews
                .AsNoTracking()
                .Include(model => model.Department)
                .Include(model => model.Customer)
                .OrderByDescending(model => model.CreatedDate)
                .ToListAsync();
        }

        public async Task<List<Review>> GetFilteredReviewAsync(int? deptId, int? rating, string? sentiment)
        {
            var query = _context.Reviews
                .Include(m => m.Department)
                .Include(m => m.Customer)
                .AsQueryable();

            if (deptId.HasValue) { query = query.Where(m => m.DeptId == deptId.Value); }
            else if (rating.HasValue) { query = query.Where(m => m.Rating == rating.Value); }
            else if (!string.IsNullOrWhiteSpace(sentiment))
            {
                var param = sentiment.Trim().ToLower();

                query = param switch
                {
                    "positive" => query = query.Where(m => m.Rating >= 5),
                    "negative" => query = query.Where(m => m.Rating <= 3),
                    "neutral" => query = query.Where(m => m.Rating == 4),
                    _ => query.Where(m => false)
                };
            }

            var listdata = await query
                    .OrderByDescending(m => m.CreatedDate)
                    .ToListAsync();

            return listdata;
        }

        public async Task<List<AppUser>> FetchUserInfo()
        {
            var users =  await _userManager.GetUsersInRoleAsync("Customer");
            return users.ToList();
        }
    }
}
