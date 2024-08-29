using CharityHub.Business.Services.TokenRepository;
using CharityHub.Business.ViewModels;
using CharityHub.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CharityHub.WebAPI.Controllers.Register
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoUserController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly ITokenRepository tokenRepository;

        public NoUserController(UserManager<User> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        //POST: /api/NoUser/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new User
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username,
                DisplayName = registerRequestDto.DisplayName,
                DateCreated = DateTime.Now,
                LastLoginDate = DateTime.Now
            };

            identityUser.IsActive = true;

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                //Add roles to this User

                identityResult = await userManager.AddToRoleAsync(identityUser, "User");

                if (identityResult.Succeeded)
                {
                    return Ok();
                }

            }

            return BadRequest("Something went wrong!");
        }

        [HttpGet]
        [Route("CheckEmail")]
        public async Task<IActionResult> CheckEmail([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email is required");
            }

            var user = await userManager.FindByEmailAsync(email);

            if (user != null)
            {
                return Ok(new { exists = true });
            }
            else
            {
                return Ok(new { exists = false });
            }
        }
    }
}
