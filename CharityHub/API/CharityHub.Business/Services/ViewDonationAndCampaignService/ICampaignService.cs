using CharityHub.Business.ViewModels;

namespace CharityHub.Business.Services.ViewDonationAndCampaignService
{
    public interface ICampaignService
    {
        Task<List<CampaignCardDto>> GetAllCampaignsAsync();
        public Task<List<dynamic>> GetAllCampaignsExceptNewAsync();

        public Task<int> GetCampaignCodeByDonationIdAsync(Guid donationId);

        public Task<string> GetCampaignStatusAsync(Guid campaignId);
        Task<List<DonationInfo>> GetConfirmedDonationsByCampaignIdAsync(Guid campaignId);
    }
}
