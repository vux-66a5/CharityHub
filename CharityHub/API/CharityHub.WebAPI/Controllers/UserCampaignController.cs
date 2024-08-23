using CharityHub.Data.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CharityHub.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class UserCampaignController : ControllerBase
    {
        private readonly CharityHubDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserCampaignController(CharityHubDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // Xem danh sách các lần đã quyên góp
        [HttpGet("donations")]
        public async Task<IActionResult> GetUserDonations()
        {
            var userIdString = _httpContextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userIdString == null || !Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }

            var donations = await (from d in _context.Donations
                                   join c in _context.Campaigns on d.CampaignId equals c.CampaignId
                                   where d.UserId == userId
                                   select new
                                   {
                                       CampaignTitle = c.CampaignTitle,
                                       d.Amount,
                                       d.DateDonated
                                   }).ToListAsync();

            return Ok(donations);
        }

        // các chức năng khác có thể dùng chung với NoUserViewCampaign
    }
}
