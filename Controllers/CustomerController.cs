using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using QualityWebSystem.DTOs;
using QualityWebSystem.Hubs;
using QualityWebSystem.Services;
using System.Security.Claims;

namespace QualityWebSystem.Controllers
{
    [Route("api/customer")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CustomerController : ControllerBase
    {
        private readonly IReviewService _service;
        private readonly IHubContext<AdminReviewAlertHub> _hub;

        public CustomerController(IReviewService service, IHubContext<AdminReviewAlertHub> hub)
        {
            _service = service; _hub = hub;
        }

        // add review api
        [HttpPost("addMyReview")]
        public async Task<IActionResult> CreateReview([FromBody]  CreateReviewDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("invalid model");

            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(customerId))
                return Unauthorized("Invalid token");

            await _service.AddReviewAsync(dto, customerId);
            //realtime notificaton sending to admin..
            if (dto.Rating >= 4)
            {
                await _hub.Clients.Group("Admins").SendAsync("ReceiveAlert", "positive");
            }
            else if (dto.Rating ==3)
            {
                await _hub.Clients.Group("Admins").SendAsync("ReceiveAlert", "neutral");
            }
            else if (dto.Rating <= 2)
            {
                await _hub.Clients.Group("Admins").SendAsync("ReceiveAlert", "negative");
            }
            return Ok("Review submitted successfully!");
        }

        //get all review list using customerId api
        [HttpGet("getMyReviews")]
        public async Task<IActionResult> GetMyReviews()
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (customerId == null)
                return Unauthorized("Unauthorize access.");

            var reviews = await _service.GetCustomerReviewsAsync(customerId);

            return Ok(reviews);
        }

        //edit review
        [HttpPut("updateReview/{id}")]
        public async Task<IActionResult> UpdateReview(int id, EditReviewDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _service.UpdateReviewAsync(id, dto, userId);

            if (!result)
                return Problem("Something went wrong. Please try again later.");

            return Ok("Review updated successfully!");
        }


        //delete review
        //[HttpDelete("deleteReview/{id}")]
        //public async Task<IActionResult> DeleteReview(int id)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    var isDeleted = await _service.DeleteCustomerReviewAsync(id, userId);

        //    if (!isDeleted)
        //        return Problem("Something went wrong! Invalid Id provided.");

        //    return Ok("Review deleted successfully!");
        //}
    }
}
