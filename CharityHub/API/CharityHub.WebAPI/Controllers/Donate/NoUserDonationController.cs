using CharityHub.Business.Services.PayPalDonate;
using CharityHub.Business.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CharityHub.WebAPI.Controllers.Donations
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoUserDonationController : ControllerBase
    {
        private readonly INoUserDonationService noUserDonationService;

        public NoUserDonationController(INoUserDonationService noUserDonationService)
        {
            this.noUserDonationService = noUserDonationService;
        }

        // POST: api/NoUserDonation/paypal/create
        [HttpPost("Create-PayPal-Donation")]
        public async Task<IActionResult> CreatePayPalDonation([FromBody] AddDonationRequestDto donationRequest)
        {
            try
            {
                var paymentUrl = await noUserDonationService.CreatePayPalDonationAsync(donationRequest);
                return Ok(new { PaymentUrl = paymentUrl });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // GET: api/NoUserDonation/ExecutePayment
        [HttpGet("Execute-Payment")]
        public async Task<IActionResult> ExecutePayment()
        {
            return await noUserDonationService.ExecutePaymentAsync(Request.Query);
        }
    }
}
