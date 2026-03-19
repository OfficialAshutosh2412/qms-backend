using QualityWebSystem.DTOs;
using QualityWebSystem.Repositories;

namespace QualityWebSystem.Services
{
    public class SharedService : ISharedService
    {
        private readonly ISharedRepo _repo;

        public SharedService(ISharedRepo repo)
        {
            _repo = repo;
        }

        public async Task<List<DepartmentDTO>> GetDepartmentsAsync()
        {
            var list = await _repo.GetDeptAsync();
            return list.Select(d => new DepartmentDTO { 
                DeptId=d.DeptId,
                DeptName=d.DeptName
            }).ToList();
        }
        public async Task<EditReviewDTO> GetReviewByIdAsync(int id)
        {
            var data = await _repo.GetReviewAsync(id);
            if (data == null)
                throw new KeyNotFoundException($"Review with id {id} not found");
            var newData = new EditReviewDTO
            {
                Rating = data.Rating,
                Description = data.Description,
                DeptId=data.DeptId
            };
            return newData;
        }

        public async Task<FetchReviewRepliesDTO> GetReviewReplyByIdAsync(int id)
        {
            var data = await _repo.GetReplyAsync(id);
            if (data == null) return null;
            var newData = new FetchReviewRepliesDTO
            {
                ReviewId=data.ReviewId,
                ReplyId=data.ReplyId,
                ReplyMessage=data.ReplyMessage,
                ReplyDate=data.ReplyDate,
            };
            return newData;
        }

        public async Task<ProfileDTO> GetCustomerProfileAsync(string id)
        {
            var result = await _repo.GetProfileAsync(id);
            if (result == null) return null;
            return new ProfileDTO
            {
                Id = result.Id,
                Email = result.Email,
                FullName = result.FullName,
                CreatedAt = result.CreatedAt
            };

        }
    }
}
