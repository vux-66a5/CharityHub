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

        // danh sách những người đã quyên góp vào 1 đợt quyên góp
        [HttpGet("campaigns/{id}/donors")]
        public async Task<IActionResult> GetDonorsCampaignId(Guid id)
        {
            var donors = await (from d in dbContext.Donations
                                join u in dbContext.Users on d.UserId equals u.Id
                                where d.CampaignId == id
                                select new
                                {
                                    u.UserName,
                                    d.Amount
                                }).ToListAsync();

            return Ok(donors);
        }

        // xem số tiền đã quyên góp và số tiền cần quyên góp
        [HttpGet("campaigns/{id}/amounts")]
        public async Task<IActionResult> GetCampaignsAmounts(Guid id)
        {
            var campaign = await dbContext.Campaigns
                .Where(c => c.CampaignId == id)
                .Select(c => new
                {
                    c.TargetAmount,
                    CurrentAmount = dbContext.Donations
                    .Where(d => d.CampaignId == id)
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
