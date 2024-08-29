using AutoMapper;
using CharityHub.Business.Services;
using CharityHub.Business.Services.AdminCampaignService;
using CharityHub.Business.ViewModels;
using CharityHub.Data.Data;
using CharityHub.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CharityHub.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;
        private readonly CharityHubDbContext dbContext;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public AdminController(IAdminService adminService, CharityHubDbContext dbContext, UserManager<User> userManager, IMapper mapper)
        {
            this.adminService = adminService;
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [HttpGet("Get-All-Users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userManager.Users.ToListAsync();

            // Lọc người dùng có vai trò là "User"
            var filteredUsers = users.Where(user => userManager.GetRolesAsync(user).Result.Contains("User")).ToList();

            // Ánh xạ và trả về danh sách người dùng
            return Ok(mapper.Map<List<UserDto>>(filteredUsers));
        }

        // GET: api/User/search?emailOrPhone=xxx
        [HttpGet("Search-User")]
        public async Task<IActionResult> SearchUser([FromQuery] string? query)
        {
            var result = await adminService.SearchUserAsync(query);
            return Ok(result);
        }

        // PUT: api/User/activate/{userId}
        [HttpPut("Activate-User/{userId}")]
        public async Task<IActionResult> ActivateUser([FromRouteAttribute] string userId, [FromBody] bool isActive)
        {
            try
            {
                var message = await adminService.ActivateUserAsync(userId, isActive);
                return Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("Activate-Users")]
        public async Task<IActionResult> ActivateUsers([FromQuery] List<string> userIds, [FromBody] bool isActive)
        {
            try
            {
                var message = await adminService.ActivateUsersAsync(userIds, isActive);
                return Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //GET: api/User/paginated?pageNumber=1&pageSize=20
        [HttpGet("Get-Users")]
        public async Task<IActionResult> GetUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var userDtos = await adminService.GetUsersAsync(pageNumber, pageSize);
            return Ok(userDtos);
        }
    }
}
