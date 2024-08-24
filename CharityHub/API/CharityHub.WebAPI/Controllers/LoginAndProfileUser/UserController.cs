using CharityHub.Business.Services.TokenRepository;
using CharityHub.Business.ViewModels;
using CharityHub.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CharityHub.WebAPI.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenRepository tokenRepository;
        private readonly IHttpContextAccessor _httpContext;

        public UserController(UserManager<User> userManager, ITokenRepository tokenRepository, IHttpContextAccessor httpContext)
        {
            _userManager = userManager;
            this.tokenRepository = tokenRepository;
            _httpContext = httpContext;
        }

        //POST: /api/User/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    user.LastLoginDate = DateTime.UtcNow;
                    await _userManager.UpdateAsync(user);

                    //Get Roles for this user
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        //Create Token
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(response);
                    }
                }
            }

            return BadRequest("Username or password incorrect!");
        }

        // Đổi mật khẩu
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordViewModel passwordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(_httpContext.HttpContext.User);

                if (user == null) return Unauthorized();

                var result = await _userManager.ChangePasswordAsync(user, passwordViewModel.CurrentPassword, passwordViewModel.NewPassword);

                if (result.Succeeded)
                {
                    ModelState.Clear();
                    return Ok("Password changed successfully!");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return BadRequest(ModelState);
        }

        // Profile
        [Authorize(Roles = "User")]
        [HttpGet("Profile")]
        public async Task<IActionResult> GetProfile()
        {
            var user = await _userManager.GetUserAsync(_httpContext.HttpContext.User);
            
            if (user == null) return NotFound();

            var userProfile = new ProfileUserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                PhoneNumber = user.PhoneNumber
            };

            return Ok(userProfile);
        }

        // Update Profile: DisplayName and PhoneNumber
        [Authorize(Roles = "User")]
        [HttpPut("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequestDto updateProfileRequestDto)
        {
            var user = await _userManager.GetUserAsync(_httpContext.HttpContext.User);

            if (user == null) return NotFound();

            if (!string.IsNullOrEmpty(updateProfileRequestDto.DisplayName) && updateProfileRequestDto.DisplayName != "string")
            {
                user.DisplayName = updateProfileRequestDto.DisplayName;
            }

            if (!string.IsNullOrEmpty(updateProfileRequestDto.PhoneNumber) && updateProfileRequestDto.PhoneNumber != "string")
            {
                user.PhoneNumber = updateProfileRequestDto.PhoneNumber;
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating user profile.");
            }

            return Ok("Profile updated successfully.");
        }
    }
}
