using CharityHub.Business.ViewModels;

namespace CharityHub.Business.Services.ViewCampaignService
{
    public interface INoUserViewCampaignService
    {
        Task<List<CampaignDto>> GetCampaignsByStatusAsync(string status);
        Task<List<DonorDto>> GetDonorsByCampaignCodeAsync(int code);
        Task<CampaignAmountsDto> GetCampaignAmountsByCampaignCodeAsync(int code);
    }
}
