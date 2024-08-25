using AutoMapper;
using CharityHub.Business.ViewModels;
using CharityHub.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CharityHub.Business.Services
{
    public class AdminService: IAdminService
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public AdminService(UserManager<User> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<UserDto> SearchUserAsync(string emailOrPhone)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(
                u => u.Email == emailOrPhone || u.PhoneNumber == emailOrPhone);

            if (user == null)
            {
                throw new Exception("User not found!");
            }

            return mapper.Map<UserDto>(user);
        }

        public async Task<string> ActivateUserAsync(string userId, bool isActive)
        {
            var user = await userManager.FindByIdAsync(userId);
            var roles = await userManager.GetRolesAsync(user);

            if (user == null)
            {
                throw new Exception("User not found!");
            }

            if (!roles.Contains("User"))
            {
                throw new Exception("Only users with the 'User' role can be updated.");
            }

            if (string.IsNullOrEmpty(user.SecurityStamp))
            {
                user.SecurityStamp = Guid.NewGuid().ToString();
            }

            user.IsActive = isActive;
            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return $"Account has been {(isActive ? "activated" : "locked")}.";
            }

            throw new Exception($"Update failed for user with ID {userId}. Errors: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        public async Task<string> ActivateUsersAsync(List<string> userIds, bool isActive)
        {
            if (userIds == null || userIds.Count == 0)
            {
                throw new Exception("No user IDs provided!");
            }

            var errors = new List<string>();

            foreach (var userId in userIds)
            {
                var user = await userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    errors.Add($"User with ID {userId} does not exist.");
                    continue;
                }

                var roles = await userManager.GetRolesAsync(user);

                if (!roles.Contains("User"))
                {
                    errors.Add($"User with ID {userId} does not have the 'User' role.");
                    continue;
                }

                if (string.IsNullOrEmpty(user.SecurityStamp))
                {
                    user.SecurityStamp = Guid.NewGuid().ToString();
                }

                user.IsActive = isActive;
                var result = await userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    errors.Add($"Update failed for user with ID {userId}. Errors: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }

            if (errors.Count > 0)
            {
                return $"Errors occurred while updating users: {string.Join(", ", errors)}";
            }

            return $"All accounts have been {(isActive ? "activated" : "deactivated")}.";
        }

        public async Task<List<UserDto>> GetUsersAsync(int pageNumber, int pageSize)
        {
            var userRole = await userManager.GetUsersInRoleAsync("User");

            var users = userRole.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return mapper.Map<List<UserDto>>(users);
        }
    }
}
