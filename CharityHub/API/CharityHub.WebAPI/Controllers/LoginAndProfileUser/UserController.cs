using CharityHub.Business.Services.TokenRepository;
using CharityHub.Business.ViewModels;
using CharityHub.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
            var user = await _userManager.FindByEmailAsync(loginRequestDto.UserName);

            if (user != null && user.IsActive == true)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    user.LastLoginDate = DateTime.Now;
                    await _userManager.UpdateAsync(user);

                    //Get Roles for this user
                    var roles = await _userManager.GetRolesAsync(user);
                    var role = roles.FirstOrDefault();
                    if (roles != null)
                    {
                        //Create Token
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            UserName = loginRequestDto.UserName,
                            Role = role,
                            JwtToken = jwtToken,
                            Id = user.Id
                        };

                        return Ok(response);
                    }
                }
            }

            return BadRequest("Username hoac password khong dung hoac tai khoan da bi khoa!");
        }

        // Đổi mật khẩu
        [Authorize(Roles = "User")]
        [HttpPut("Change-Password")]
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
                    return Ok("Password thay doi thanh cong!");
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
        [HttpGet("Get-Profile/{id}")]
        public async Task<IActionResult> GetProfile(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null) return NotFound();

            var userProfile = new ProfileUserDto
            {
                UserName = user.UserName,
                DisplayName = user.DisplayName,
                PhoneNumber = user.PhoneNumber
            };

            return Ok(userProfile);
        }


        // Update Profile: DisplayName and PhoneNumber
        [Authorize(Roles = "User")]
        [HttpPut("Update-Profile/{id}")]
        public async Task<IActionResult> UpdateProfile(Guid id, [FromBody] UpdateProfileRequestDto updateProfileRequestDto)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

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

            return Ok("Profile da duoc cap nhat thanh cong.");
        }
    }
}
