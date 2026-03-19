using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QualityWebSystem.Data;
using QualityWebSystem.Models;

namespace QualityWebSystem.Repositories
{
    public class SharedRepo : ISharedRepo
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public SharedRepo(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<Department>> GetDeptAsync()
        {
            return await _context.Departments
                .AsNoTracking().ToListAsync();
        }
        public async Task<Review> GetReviewAsync(int id)
        {
            return await _context.Reviews.FirstOrDefaultAsync(d => d.ReviewId == id);
        }

        public async Task<ReviewReply> GetReplyAsync(int id)
        {
            return await _context.ReviewReplies.FirstOrDefaultAsync(d => d.ReviewId == id);
        }
        public async Task<AppUser> GetProfileAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }
    }
}
