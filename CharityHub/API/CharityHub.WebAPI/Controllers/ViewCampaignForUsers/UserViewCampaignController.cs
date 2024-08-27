using CharityHub.Business.Services.ViewCampaignService;
using CharityHub.Data.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CharityHub.WebAPI.Controllers.ViewCampaignForUsers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class UserViewCampaignController : ControllerBase
    {
        private readonly IUserViewCampaignService userViewCampaignService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserViewCampaignController(IUserViewCampaignService userViewCampaignService, IHttpContextAccessor httpContextAccessor)
        {
            this.userViewCampaignService = userViewCampaignService;
            _httpContextAccessor = httpContextAccessor;
        }

        // Xem danh sách các lần đã quyên góp
        [HttpGet("Get-User-Donations")]
        public async Task<IActionResult> GetUserDonations()
        {
            var userIdString = _httpContextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userIdString == null || !Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }

            var donations = await userViewCampaignService.GetUserDonationsAsync(userId);
            return Ok(donations);
        }

        // các chức năng khác có thể dùng chung với NoUserViewCampaign
    }
}
