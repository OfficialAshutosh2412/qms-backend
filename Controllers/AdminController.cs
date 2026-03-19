using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using QualityWebSystem.DTOs;
using QualityWebSystem.Hubs;
using QualityWebSystem.Repositories;
using QualityWebSystem.Services;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace QualityWebSystem.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _service;
        private readonly IReviewReplyService _reviewReplyService;
        private readonly IAdminDashboardService _dashboardService;

        public AdminController(IAdminService service, IReviewReplyService reviewReplyService, IAdminDashboardService dashboardService
            )
        {
            _service = service;
            _reviewReplyService = reviewReplyService;
            _dashboardService = dashboardService;

        }
        //dashboard api
        [HttpGet("dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            int totalReviewsCount = await _dashboardService.GetReviewsCountAsync();
            var deptSummary = await _dashboardService.GetDepartmentSummaryAsync();
            var sentimentData = await _dashboardService.GetSentimentDataAsync();
            return Ok(new { totalReviewsCount = totalReviewsCount, departmentSummary = deptSummary, sentimentData = sentimentData });

        }

        //get reviews
        [HttpGet("getAllReviews")]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviewList = await _service.GetAllReviewsAsync();
            if (reviewList == null)
                return NotFound("No data found");

            return Ok(reviewList);
        }

        //filter api
        [HttpGet("reviews")]
        public async Task<IActionResult> Filtering(int? deptId, int? rating, string? sentiment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (rating.HasValue && (rating < 1 || rating > 5))
                return BadRequest("Invalid rating provided.");

            if (!string.IsNullOrWhiteSpace(sentiment))
            {
                var thresholds = new[] { "positive", "negative", "neutral" };
                if (!thresholds.Contains(sentiment.Trim().ToLower()))
                {
                    return BadRequest("Wrong thresholds provided.");
                }
            }
            int filterCount = (deptId.HasValue ? 1 : 0) + (rating.HasValue ? 1 : 0) + (!string.IsNullOrWhiteSpace(sentiment) ? 1 : 0);

            if (filterCount == 0)
                return BadRequest("At least one filter parameter is required.");

            if (filterCount > 1)
                return BadRequest("Only one filter parameter is allowed at a time.");

            var data = await _service.GetFilteredReviewServiceAsync(deptId, rating, sentiment);

            if (!data.Any())
                return NoContent();

            return Ok(data);
        }

        //user info api
        [HttpGet("user-info")]
        public async Task<IActionResult> UserInfo()
        {
            var list = await _service.GetUserInfoAsync();
            if (!list.Any())
                return NoContent();

            return Ok(list);
        }

        //review reply api
        [HttpPost("reply-to-review")]
        public async Task<IActionResult> Reply(ReviewReplyDTO dto)
        {
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (adminId == null)
                return Unauthorized();

            var result = await _reviewReplyService.PostReplyAsync(dto, adminId);
            if (!result)
                return Problem("Something went wrong.");

            return Ok("Reply posted sucessfully!");
        }

        //get all replies
        [HttpGet("fetch-replies")]
        public async Task<IActionResult> FetchReply()
        {
            var result = await _reviewReplyService.FetchReplyReviewsAsync();
            if (result.Count == 0)
                return NoContent();

            return Ok(result);
        }
    }
}
