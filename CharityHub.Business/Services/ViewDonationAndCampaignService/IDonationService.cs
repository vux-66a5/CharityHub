

using CharityHub.Business.ViewModels;

namespace CharityHub.Business.Services.ViewDonationAndCampaignService
{
    public interface IDonationService
    {
        Task<List<DonationDetailsDto>> GetDonationDetailsAsync();
        Task<List<DonationDetailsDto>> GetDonationDetailsByDisplayNameAndCampaignCodeAsync(string search);

    }
}
