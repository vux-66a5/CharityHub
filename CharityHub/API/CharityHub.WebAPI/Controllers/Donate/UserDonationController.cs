
using CharityHub.Business.Services.PayPalDonate;
using CharityHub.Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CharityHub.WebAPI.Controllers.Donations
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class UserDonationController : ControllerBase
    {
        private readonly IUserDonationService userDonationService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserDonationController(IUserDonationService userDonationService, IHttpContextAccessor httpContextAccessor)
        {
            this.userDonationService = userDonationService;
            this.httpContextAccessor = httpContextAccessor;
        }

        // POST: api/NoUserDonation/paypal/create
        [HttpPost("paypal/create")]
        public async Task<IActionResult> CreatePayPalDonation([FromBody] AddDonationRequestDto donationRequest)
        {
            var userIdString = httpContextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return BadRequest("User khong ton tai.");
            }

            try
            {
                var paymentUrl = await userDonationService.CreatePayPalDonationAsync(donationRequest, userId);
                return Ok(new { PaymentUrl = paymentUrl });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/NoUserDonation/ExecutePayment
        [HttpGet("ExecutePayment")]
        public async Task<IActionResult> ExecutePayment()
        {
            var userIdString = httpContextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return BadRequest("User khong ton tai.");
            }

            return await userDonationService.ExecutePaymentAsync(Request.Query, userId);
        }
    }
}
