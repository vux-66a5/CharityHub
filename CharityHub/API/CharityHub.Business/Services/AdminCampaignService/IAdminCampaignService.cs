using CharityHub.Business.ViewModels;
using CharityHub.Data.Models;

namespace CharityHub.Business.Services.AdminCampaignService
{
    public interface IAdminCampaignService
    {
        Task<List<CampaignDto>> SearchCampaignsAsync(string query);
        Task<List<Campaign>> GetAllCampaignAsync();
        Task<CampaignDto> CreateCampaignAsync(AddCampaignRequestDto addCampaignRequestDto);
        Task<IEnumerable<CampaignDto>> SearchCampaignsByStatusAsync(string status);
        Task<IEnumerable<CampaignDto>> SearchCampaignsByPhoneAsync(string phoneNumber);
        Task<IEnumerable<CampaignDto>> SearchCampaignsByCodeAsync(int campaignCode);
        Task<string> DeleteNewCampaignAsync(int campaignCode);
        Task<string> DeleteCampaignAsync(Guid campaignId);
        Task<CampaignDto> UpdateCampaignAsync(Guid campaignId, UpdateCampaignRequestDto updatedCampaign);
        Task<object> GetDonationProgressAsync(int campaignCode);
        Task<string> ExtendCampaignEndDateAsync(int campaignCode, DateTime newEndDate);
        Task<CampaignDto> UpdateStartAndEndDateAsync(Guid campaignId, StartAndEndDateCampaign startAndEndDateCampaign);
    }
}