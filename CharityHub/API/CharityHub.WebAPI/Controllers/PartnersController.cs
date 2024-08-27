using CharityHub.Business.ViewModels;
using CharityHub.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CharityHub.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnersController : ControllerBase
    {
        private readonly CharityHubDbContext dbContext;

        public PartnersController(CharityHubDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Danh sách các nhà tài trợ
        [HttpGet("Partner-Infor")]
        public async Task<IActionResult> GetPartnerInfor()
        {
            var partners = dbContext.Campaigns
                .Select(c => new PartnerViewModel
                {
                    PartnerName = c.PartnerName,
                    PartnerLogo = c.PartnerLogo
                }).Distinct().ToList();

            if (!partners.Any())
            {
                return NotFound();
            }

            return Ok(partners);
        }

        // danh sách các chiến dịch do nhà tài trợ đó hỗ trợ
        [HttpGet("Campaigns-By-Partner/{partnerName}")]
        public async Task<IActionResult> GetCampaignByPartner(string partnerName)
        {
            var campaigns = await dbContext.Campaigns
                .Where(c => c.PartnerName == partnerName)
                .Select(c => new CampaignCardDto
                {
                    CampaignTitle = c.CampaignTitle,
                    CampaignCode = c.CampaignCode,
                    CampaignThumbnail = c.CampaignThumbnail,
                    CampaignDescription = c.CampaignDescription,
                    TargetAmount = c.TargetAmount,
                    CurrentAmount = c.CurrentAmount,
                    PartnerLogo = c.PartnerLogo,
                    PartnerName = c.PartnerName,
                    EndDate = c.EndDate,
                    StartDate = c.StartDate,
                    ConfirmedDonationCount = c.Donations.Count(d => d.IsConfirm)
                })
                .ToListAsync();

            if (!campaigns.Any())
            {
                return NotFound();
            }

            return Ok(campaigns);
        }
    }
}
