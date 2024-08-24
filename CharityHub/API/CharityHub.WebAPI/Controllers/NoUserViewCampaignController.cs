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
                .Where( c => c.CampaignStatus == status)
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

            var donors = await (from d in dbContext.Donations
                                join u in dbContext.Users on d.UserId equals u.Id
                                where d.CampaignId == campaign.CampaignId
                                select new
                                {
                                    u.UserName,
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
                    CurrentAmount = dbContext.Donations
                    .Where(d => d.CampaignId == c.CampaignId)
                    .Sum(d => d.Amount)
                }).FirstOrDefaultAsync();

            if (campaign == null)
            {
                return NotFound();
            }

            return Ok(campaign);
        }
    }
}
