using CharityHub.Business.ViewModels;

namespace CharityHub.Business.Services.AdminCampaignService
{
    public interface IAdminCampaignService
    {
        Task<CampaignDto> CreateCampaignAsync(AddCampaignRequestDto addCampaignRequestDto);
        Task<IEnumerable<CampaignDto>> SearchCampaignsByStatusAsync(string status);
        Task<IEnumerable<CampaignDto>> SearchCampaignsByPhoneAsync(string phoneNumber);
        Task<IEnumerable<CampaignDto>> SearchCampaignsByCodeAsync(int campaignCode);
        Task<string> DeleteNewCampaignAsync(int campaignCode);
        Task<string> DeleteCampaignAsync(int campaignCode);
        Task<CampaignDto> UpdateCampaignAsync(int campaignCode, UpdateCampaignRequestDto updatedCampaign);
        Task<object> GetDonationProgressAsync(int campaignCode);
        Task<string> ExtendCampaignEndDateAsync(int campaignCode, DateTime newEndDate);
        Task<CampaignDto> UpdateStartAndEndDateAsync(int campaignCode, StartAndEndDateCampaign startAndEndDateCampaign);
    }
}
