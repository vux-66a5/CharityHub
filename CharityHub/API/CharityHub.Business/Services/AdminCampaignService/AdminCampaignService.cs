using AutoMapper;
using CharityHub.Business.ViewModels;
using CharityHub.Data.Data;
using CharityHub.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CharityHub.Business.Services.AdminCampaignService
{
    public class AdminCampaignService : IAdminCampaignService
    {
        private readonly CharityHubDbContext dbContext;
        private readonly IMapper mapper;

        public AdminCampaignService(CharityHubDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<CampaignDto> CreateCampaignAsync(AddCampaignRequestDto addCampaignRequestDto)
        {
            var campaign = mapper.Map<Campaign>(addCampaignRequestDto);

            campaign.CampaignCode = CampaignCodeGenerator.GenerateUniqueCampaignCode(dbContext);
            campaign.DateCreated = DateTime.Now;
            campaign.CurrentAmount = 0;

            await dbContext.Campaigns.AddAsync(campaign);
            await dbContext.SaveChangesAsync();

            return mapper.Map<CampaignDto>(campaign);
        }

        public async Task<string> DeleteCampaignAsync(int campaignCode)
        {
            var campaign = await dbContext.Campaigns
            .FirstOrDefaultAsync(c => c.CampaignCode == campaignCode);
            if (campaign == null)
            {
                return "Campaign khong ton tai!";
            }

            dbContext.Campaigns.Remove(campaign);
            await dbContext.SaveChangesAsync();

            return "Campaign da duoc xoa thanh cong!";
        }

        public async Task<string> DeleteNewCampaignAsync(int campaignCode)
        {
            var campaign = await dbContext.Campaigns
                .FirstOrDefaultAsync(c => c.CampaignCode == campaignCode && c.CampaignStatus.ToLower() == "New".ToLower());
            if (campaign == null)
            {
                return "Campaign khong ton tai!";
            }

            dbContext.Campaigns.Remove(campaign);
            await dbContext.SaveChangesAsync();

            return "Campaign da duoc xoa thanh cong!";
        }

        public async Task<string> ExtendCampaignEndDateAsync(int campaignCode, DateTime newEndDate)
        {
            var campaign = await dbContext.Campaigns
                .FirstOrDefaultAsync(c => c.CampaignCode == campaignCode);

            if (campaign == null)
            {
                return "Campaign khong ton tai.";
            }

            if (newEndDate <= campaign.EndDate)
            {
                return "Campaign phai co EndDate moi muon hon EndDate hien tai.";
            }

            campaign.EndDate = newEndDate;
            await dbContext.SaveChangesAsync();

            return "Campaign end date da duoc mo rong thanh cong.";
        }

        public async Task<object> GetDonationProgressAsync(int campaignCode)
        {
            var campaign = await dbContext.Campaigns
                .Where(c => c.CampaignCode == campaignCode)
                .Select(c => new
                {
                    c.TargetAmount,
                    c.CurrentAmount
                })
                .FirstOrDefaultAsync();

            return campaign;
        }

        public async Task<IEnumerable<CampaignDto>> SearchCampaignsByCodeAsync(int campaignCode)
        {
            var campaigns = await dbContext.Campaigns
                .Where(c => c.CampaignCode == campaignCode)
                .ToListAsync();

            return mapper.Map<IEnumerable<CampaignDto>>(campaigns);
        }

        public async Task<IEnumerable<CampaignDto>> SearchCampaignsByPhoneAsync(string phoneNumber)
        {
            var campaigns = await dbContext.Campaigns
                .Where(c => c.PartnerNumber == phoneNumber)
                .ToListAsync();

            return mapper.Map<IEnumerable<CampaignDto>>(campaigns);
        }

        public async Task<IEnumerable<CampaignDto>> SearchCampaignsByStatusAsync(string status)
        {
            var campaigns = await dbContext.Campaigns
                .Where(c => c.CampaignStatus == status)
                .ToListAsync();

            return mapper.Map<IEnumerable<CampaignDto>>(campaigns);
        }

        public async Task<CampaignDto> UpdateCampaignAsync(int campaignCode, UpdateCampaignRequestDto updatedCampaign)
        {
            var campaignModel = mapper.Map<Campaign>(updatedCampaign);

            var existingCampaign = await dbContext.Campaigns
                .FirstOrDefaultAsync(c => c.CampaignCode == campaignCode);

            if (existingCampaign == null) return null;

            if (existingCampaign.CurrentAmount > 0)
                return null;

            existingCampaign.CampaignTitle = campaignModel.CampaignTitle;
            existingCampaign.CampaignDescription = campaignModel.CampaignDescription;
            existingCampaign.CampaignThumbnail = campaignModel.CampaignThumbnail;
            existingCampaign.TargetAmount = campaignModel.TargetAmount;
            existingCampaign.CurrentAmount = campaignModel.CurrentAmount;
            existingCampaign.StartDate = campaignModel.StartDate;
            existingCampaign.EndDate = campaignModel.EndDate;
            existingCampaign.PartnerName = campaignModel.PartnerName;
            existingCampaign.PartnerNumber = campaignModel.PartnerNumber;
            existingCampaign.PartnerLogo = campaignModel.PartnerLogo;
            existingCampaign.CampaignStatus = campaignModel.CampaignStatus;

            await dbContext.SaveChangesAsync();
            return mapper.Map<CampaignDto>(existingCampaign);
        }

        public async Task<CampaignDto> UpdateStartAndEndDateAsync(int campaignCode, StartAndEndDateCampaign startAndEndDateCampaign)
        {
            var campaign = await dbContext.Campaigns
                .FirstOrDefaultAsync(c => c.CampaignCode == campaignCode);

            if (campaign == null)
            {
                return null;
            }

            if (campaign.StartDate != null || campaign.EndDate != null)
            {
                return null;
            }

            campaign.StartDate = startAndEndDateCampaign.StartDate;
            campaign.EndDate = startAndEndDateCampaign.EndDate;

            dbContext.Campaigns.Update(campaign);
            await dbContext.SaveChangesAsync();

            return mapper.Map<CampaignDto>(campaign);
        }
    }
}
