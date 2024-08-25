using CharityHub.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharityHub.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;
        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        // GET: api/User/search?emailOrPhone=xxx
        [HttpGet("search")]
        public async Task<IActionResult> SearchUser(string emailOrPhone)
        {
            try
            {
                var userDto = await adminService.SearchUserAsync(emailOrPhone);
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/User/activate/{userId}
        [HttpPut("activate/{userId}")]
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


        [HttpPut("Change-status-active")]
        public async Task<IActionResult> ActivateUser([FromQuery] List<string> userIds, [FromBody] bool isActive)
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
        [HttpGet("paginated")]
        public async Task<IActionResult> GetUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var userDtos = await adminService.GetUsersAsync(pageNumber, pageSize);
            return Ok(userDtos);
        }
    }
}
