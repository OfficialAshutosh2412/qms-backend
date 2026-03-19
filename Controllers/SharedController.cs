using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QualityWebSystem.DTOs;
using QualityWebSystem.Services;
using System.Security.Claims;

namespace QualityWebSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Customer, Admin")]
    public class SharedController : ControllerBase
    {
        private readonly ISharedService _shared;

        public SharedController(ISharedService shared)
        {
            _shared = shared;
        }
        [HttpGet("getDepartments")]
        public async Task<IActionResult> Departments()
        {
            var dep = await _shared.GetDepartmentsAsync();
            return Ok(dep);
        }
        [HttpGet("getReview/{id}")]
        public async Task<IActionResult> GetReview(int id)
        {
            try
            {var data = await _shared.GetReviewByIdAsync(id);
                return Ok(data);
            }
            catch (KeyNotFoundException ex)
            {
                 return NotFound(new { message = ex.Message });
            }
        }
        [HttpGet("getReviewReply/{id}")]
        public async Task<IActionResult> GetReviewReply(int id) {
                var data = await _shared.GetReviewReplyByIdAsync(id);
                return Ok(data);
        }

        //profile api
        [HttpGet("me")]
        public async Task<IActionResult> Profile()
        {
            var currentCustomerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(currentCustomerId))
                return Unauthorized();

            var result = await _shared.GetCustomerProfileAsync(currentCustomerId);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }

    
}
