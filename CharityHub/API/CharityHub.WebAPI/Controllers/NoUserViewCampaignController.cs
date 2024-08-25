using AutoMapper;
using CharityHub.Business.ViewModels;
using CharityHub.Data.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CharityHub.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoUserViewCampaignController : ControllerBase
    {
        private readonly CharityHubDbContext dbContext;
        private readonly IMapper mapper;

        public NoUserViewCampaignController(CharityHubDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        // Danh sách đợt quyên góp dựa và trạng thái (status)
        [HttpGet("Campaign")]
        public async Task<IActionResult> GetCampaignsByStatus(string status)
        {
            var campaigns = await dbContext.Campaigns
                .Where( c => c.CampaignStatus.ToLower() == status.ToLower())
                .ToListAsync();

            return Ok(mapper.Map<List<CampaignDto>>(campaigns));
        }

        // xem được danh sách những người đã quyên góp trên một đợt quyên góp
        [HttpGet("campaigns/{code}/donors")]
        public async Task<IActionResult> GetDonorsByCampaignCode(int code)
        {
            var campaign = await dbContext.Campaigns
                .Where(c => c.CampaignCode == code)
                .FirstOrDefaultAsync();

            if (campaign == null)
            {
                return NotFound("Campaign not found.");
            }

            var donors = await (from d in dbContext.Donations
                                join u in dbContext.Users on d.UserId equals u.Id into userGroup
                                from u in userGroup.DefaultIfEmpty()
                                where d.CampaignId == campaign.CampaignId && d.IsConfirm
                                orderby d.Amount descending
                                select new
                                {
                                    DisplayName = u != null ? u.DisplayName : "Nha hao tam",
                                    d.Amount
                                }).ToListAsync();

            return Ok(donors);
        }



        // Xem số tiền đã quyên góp và số tiền cần quyên góp 
        [HttpGet("campaigns/{code}/amounts")]
        public async Task<IActionResult> GetCampaignAmountsByCampaignCode(int code)
        {
            var campaign = await dbContext.Campaigns
                .Where(c => c.CampaignCode == code)
                .Select(c => new
                {
                    c.TargetAmount,
                    c.CurrentAmount
                }).FirstOrDefaultAsync();

            if (campaign == null)
            {
                return NotFound();
            }

            return Ok(campaign);
        }
    }
}
