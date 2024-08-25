using CharityHub.Business.ViewModels;

namespace CharityHub.Business.Services
{
    public interface IAdminService
    {
        Task<UserDto> SearchUserAsync(string emailOrPhone);
        Task<string> ActivateUserAsync(string userId, bool isActive);
        Task<string> ActivateUsersAsync(List<string> userIds, bool isActive);
        Task<List<UserDto>> GetUsersAsync(int pageNumber, int pageSize);
    }
}
