using AutoMapper;
using CharityHub.Business.ViewModels;
using CharityHub.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CharityHub.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IMapper mapper;

        public AdminController(UserManager<User> userManager, RoleManager<Role> roleManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
        }

        // GET: api/User/search?emailOrPhone=xxx
        [HttpGet("search")]
        public async Task<IActionResult> SearchUser(string emailOrPhone)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(
                u => u.Email == emailOrPhone || u.PhoneNumber == emailOrPhone);

            if (user == null)
            {
                return NotFound("User not found!");
            }

            return Ok(mapper.Map<UserDto>(user));
        }

        // PUT: api/User/activate/{userId}
        [HttpPut("activate/{userId}")]
        public async Task<IActionResult> ActivateUser([FromRouteAttribute] string userId, [FromBody] bool isActive)
        {
            var user = await userManager.FindByIdAsync(userId);
            var roles = await userManager.GetRolesAsync(user);
            
            if (user == null)
            {
                return NotFound("User not found!");
            }

            // kiểm tra xem người dùng có role là User không
            if (!roles.Contains("User"))
            {
                return BadRequest("Chỉ có thể thay đổi trạng thái của người dùng có role 'User'.");
            }

            if (string.IsNullOrEmpty(user.SecurityStamp))
            {
                user.SecurityStamp = Guid.NewGuid().ToString();
            }

            user.IsActive = isActive;
            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok($"Tài khoản đã được {(isActive ? "mở khóa" : "khóa")}.");
            }

            return BadRequest(result.Errors);
        }


        [HttpPut("Change-status-active")]
        public async Task<IActionResult> ActivateUser([FromQuery] List<string> userIds, [FromBody] bool isActive)
        {
            if (userIds == null || userIds.Count == 0)
            {
                return NotFound("User not found!");
            }

            var errors = new List<string>();

            foreach (var userId in userIds)
            {
                var user = await userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    errors.Add($"Người dùng với ID {userId} không tồn tại.");
                    continue;
                }

                var roles = await userManager.GetRolesAsync(user);

                // Kiểm tra xem người dùng có role là 'User' không
                if (!roles.Contains("User"))
                {
                    errors.Add($"Người dùng với ID {userId} không có role 'User'.");
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
                    errors.Add($"Cập nhật không thành công cho người dùng với ID {userId}. Lỗi: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }

            if (errors.Count > 0)
            {
                return BadRequest(new { Message = "Có lỗi xảy ra khi cập nhật người dùng.", Errors = errors });
            }

            return Ok($"Tất cả tài khoản đã {(isActive ? "kích hoạt" : "vô hiệu hóa")}.");
        }


        //GET: api/User/paginated?pageNumber=1&pageSize=20
        [HttpGet("paginated")]
        public async Task<IActionResult> GetUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var users = await userManager.Users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var userDtos = mapper.Map<List<UserDto>>(users);

            return Ok(userDtos);
        }
    }
}
