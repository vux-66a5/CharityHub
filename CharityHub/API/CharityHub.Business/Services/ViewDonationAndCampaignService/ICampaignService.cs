using CharityHub.Business.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CharityHub.Business.Services.ViewDonationAndCampaignService
{
    public interface ICampaignService
    {
        Task<List<CampaignCardDto>> GetAllCampaignsAsync();

        public Task<List<dynamic>> GetAllCampaignsExceptNewAsync();

        public Task<int> GetCampaignCodeByDonationIdAsync(Guid donationId);

        public Task<string> GetCampaignStatusAsync(Guid campaignId);
    }
}
