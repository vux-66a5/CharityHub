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

        public UserViewCampaignController(IUserViewCampaignService userViewCampaignService)
        {
            this.userViewCampaignService = userViewCampaignService;
        }

        // Xem danh sách các lần đã quyên góp
        [HttpGet("Get-User-Donations/{id}")]
        public async Task<IActionResult> GetUserDonations(Guid id)
        {
           
            var donations = await userViewCampaignService.GetUserDonationsAsync(id);
            return Ok(donations);
        }



        // các chức năng khác có thể dùng chung với NoUserViewCampaign
    }
}
