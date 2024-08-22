using AutoMapper;
using CharityHub.Business.ViewModels;
using CharityHub.Data.Data;
using CharityHub.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CharityHub.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminCampaignController : ControllerBase
    {
        private readonly CharityHubDbContext dbContext;
        private readonly IMapper mapper;

        public AdminCampaignController(CharityHubDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        // POST: api/Campaign
        [HttpPost]
        public async Task<IActionResult> CreateCampaign([FromBody] AddCampaignRequestDto addCampaignRequestDto)
        {
            var campaign = mapper.Map<Campaign>(addCampaignRequestDto);

            await dbContext.Campaigns.AddAsync(campaign);
            await dbContext.SaveChangesAsync();

            return Ok(mapper.Map<CampaignDto>(campaign));
        }

        // GET: api/Campaign/search?status=Active
        [HttpGet("search")]
        public async Task<IActionResult> SearchCapaigns(string status)
        {
            var campaigns = await dbContext.Campaigns
                .Where(c => c.CampaignStatus == status)
                .ToListAsync();

            return Ok(mapper.Map<List<CampaignDto>>(campaigns));
        }

        // GET: api/Campaign/searchByPhone?phoneNumber=123456789
        [HttpGet("searchByPhone")]
        public async Task<IActionResult> SearchCampaignByPhone(string phoneNumber)
        {
            var campaigns = await dbContext.Campaigns
                .Where(c => c.PartnerNumber == phoneNumber)
                .ToListAsync();
            return Ok(mapper.Map<List<CampaignDto>>(campaigns));
        }

        // GET: api/Campaign/searchByCode
        [HttpGet("searchByCode")]
        public async Task<IActionResult> SearchCampaignByCode(int campaignCode)
        {
            var campaigns = await dbContext.Campaigns
                .Where(c => c.CampaignCode == campaignCode)
                .ToListAsync();
            return Ok(mapper.Map<List<CampaignDto>>(campaigns));
        }

        // DELETE: api/Campaign/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCampaign(Guid id)
        {
            var campaign = await dbContext.Campaigns
                .FirstOrDefaultAsync(c => c.CampaignId == id && c.CampaignStatus == "New");
            if (campaign == null)
            {
                return NotFound("Campaign not found!");
            }

            dbContext.Campaigns.Remove(campaign);
            await dbContext.SaveChangesAsync();

            return Ok("Campaign deleted successfully!");
        }

        // PUT: api/Campaign/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCampaign(Guid id, UpdateCampaignRequestDto updatedCampaign)
        {
            var campaignModel = mapper.Map<Campaign>(updatedCampaign);

            var existingCampaign = await dbContext.Campaigns.FindAsync(id);
            if (existingCampaign == null) return NotFound("Campaign not found.");

            if (existingCampaign.CurrentAmount > 0)
                return BadRequest("Cannot update campaign with active donations.");

            existingCampaign.CampaignTitle = campaignModel.CampaignTitle;
            existingCampaign.CampaignDescription = campaignModel.CampaignDescription;
            existingCampaign.CampaignThumbnail = campaignModel.CampaignThumbnail;
            existingCampaign.CampaignCode = campaignModel.CampaignCode;
            existingCampaign.TargetAmount = campaignModel.TargetAmount;
            existingCampaign.CurrentAmount = campaignModel.CurrentAmount;
            existingCampaign.StartDate = campaignModel.StartDate;
            existingCampaign.EndDate = campaignModel.EndDate;
            existingCampaign.PartnerName = campaignModel.PartnerName;
            existingCampaign.PartnerNumber = campaignModel.PartnerNumber;
            existingCampaign.PartnerLogo = campaignModel.PartnerLogo;
            existingCampaign.CampaignStatus = campaignModel.CampaignStatus;

            await dbContext.SaveChangesAsync();
            return Ok(mapper.Map<CampaignDto>(existingCampaign));
        }

        // GET: api/Campaign/{id}/progress
        [HttpGet("{id}/progress")]
        public async Task<IActionResult> GetDonationProgress(Guid id)
        {
            var campaign = await dbContext.Campaigns
                .Where(c => c.CampaignId == id)
                .Select(c => new
                {
                    c.TargetAmount,
                    c.CurrentAmount
                })
                .FirstOrDefaultAsync();

            if (campaign == null)
            {
                return NotFound("Campaign not found.");
            }

            return Ok(campaign);
        }

        // PUT: api/Campaign/{id}/extend
        [HttpPut("{id}/extend")]
        public async Task<IActionResult> ExtendCampaignEndDate(Guid id, [FromBody] DateTime newEndDate)
        {
            var campaign = await dbContext.Campaigns.FindAsync(id);
            if (campaign == null)
            {
                return NotFound("Campaign not found.");
            }

            if (newEndDate <= campaign.EndDate)
            {
                return BadRequest("New end date must be later than the current end date.");
            }

            campaign.EndDate = newEndDate;
            await dbContext.SaveChangesAsync();

            return Ok("Campaign end date extended successfully.");
        }

    }
}
