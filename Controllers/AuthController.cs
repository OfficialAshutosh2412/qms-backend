using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QualityWebSystem.DTOs;
using QualityWebSystem.Models;
using QualityWebSystem.Services;

namespace QualityWebSystem.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //dependency variables
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly ITokenService _tokenService;
        //dependency injection
        public AuthController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signinManager,
            ITokenService tokenService
            )
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _tokenService = tokenService;
        }

        //register api
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO reg)
        {
            if (!ModelState.IsValid)
            {
                var modelErrors = ModelState.Values
                    .SelectMany(err => err.Errors)
                    .Select(erro => erro.ErrorMessage);

                return BadRequest(new { errors = modelErrors });
            }


            var user = await _userManager.FindByEmailAsync(reg.Email);

            if (user != null)
                return BadRequest(new { errors = new[] { "User exists already!" } });

            var newuser = new AppUser
            {
                UserName = reg.Email,
                Email = reg.Email,
                FullName = reg.FullName,
            };

            var result = await _userManager.CreateAsync(newuser, reg.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(data => data.Description);
                return BadRequest(new { error = errors });
            }

            await _userManager.AddToRoleAsync(newuser, "Customer");

            return Ok(new { message="User registration successfully done." });
        }

        //login api
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO login)
        {
            //finding user existence
            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user == null)
                return Unauthorized("Invalid username or password. Try again later!");

            var result = await _signinManager.CheckPasswordSignInAsync(user, login.Password, false);

            if (!result.Succeeded)
                return Unauthorized("Invalid username or password. Try again later!");

            var token = await _tokenService.GenerateJWTToken(user);

            return Ok(new { token });
        }
    }
}
