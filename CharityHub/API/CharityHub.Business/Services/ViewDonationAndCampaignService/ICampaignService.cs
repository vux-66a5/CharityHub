using CharityHub.Business.ViewModels;

namespace CharityHub.Business.Services.ViewDonationAndCampaignService
{
    public interface ICampaignService
    {
        Task<List<CampaignCardDto>> GetAllCampaignsAsync();
    }
}
