
using CharityHub.Business.ViewModels;

namespace CharityHub.Business.Services.ViewCampaignService
{
    public interface IUserViewCampaignService
    {
        Task<List<DonatedByMyselfDto>> GetUserDonationsAsync(Guid userId);

    }
}
