

using CharityHub.Business.ViewModels;
using CharityHub.Data.Data;
using CharityHub.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CharityHub.Business.Services.ViewDonationAndCampaignService
{
    public class CampaignService : ICampaignService
    {
        private readonly CharityHubDbContext dbContext;

        public CampaignService(CharityHubDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<CampaignCardDto>> GetAllCampaignsAsync()
        {
            var campaigns = await dbContext.Campaigns
                .Select(c => new CampaignCardDto
                {
                    CampaignId = c.CampaignId,
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

            return campaigns;
        }
    }
}
