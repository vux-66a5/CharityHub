using AutoMapper;
using CharityHub.Business.Services;
using CharityHub.Business.ViewModels;
using CharityHub.Business.VNPay.Models;
using CharityHub.Business.VNPay.Services;
using CharityHub.Data.Data;
using CharityHub.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CharityHub.WebAPI.Controllers.Donations
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoUserDonationController : ControllerBase
    {
        private readonly CharityHubDbContext dbContext;
        private readonly IPayPalService payPalService;
        private readonly IMapper mapper;
        private readonly IVnPayService _vnPayService;
        private readonly ILogger<NoUserDonationController> _logger;

        public NoUserDonationController(CharityHubDbContext dbContext, IPayPalService payPalService, IMapper mapper, IVnPayService vnPayService , ILogger<NoUserDonationController> logger)
        {
            this.dbContext = dbContext;
            this.payPalService = payPalService;
            this.mapper = mapper;
            this._vnPayService = vnPayService;
            this._logger = logger;
        }

        // POST: api/NoUserDonation/paypal/create
        [HttpPost("Create-PayPal-Donation")]
        public async Task<IActionResult> CreatePayPalDonation([FromBody] AddDonationRequestDto donationRequest)
        {
            var donationId = Guid.NewGuid();

            var campaign = await dbContext.Campaigns
                .Where(c => c.CampaignCode == donationRequest.CampaignCode)
                .FirstOrDefaultAsync();

            var donation = new Donation
            {
                CampaignId = campaign.CampaignId,
                DonationId = donationId,
                DateDonated = DateTime.Now,
                IsConfirm = false,
                Amount = donationRequest.Amount,
                PaymentMethod = donationRequest.PaymentMethod
            };

            var paymentUrl = await payPalService.CreatePaymentUrl(new PaymentInformation
            {
                Amount = donation.Amount,
                DonationId = donationId // Pass DonationId to PayPal
            });

            if (string.IsNullOrEmpty(paymentUrl))
            {
                return BadRequest("Unable to create PayPal payment.");
            }

            dbContext.Donations.Add(donation);
            await dbContext.SaveChangesAsync();

            return Ok(new { PaymentUrl = paymentUrl });
        }

        [HttpPost("Create-VnPay-Donation")]
        public async Task<IActionResult> CreateVnPayDonation([FromBody] AddDonationRequestDto donationRequest)
        {
            var donationId = Guid.NewGuid();

            var campaign = await dbContext.Campaigns
                .Where(c => c.CampaignCode == donationRequest.CampaignCode)
                .FirstOrDefaultAsync();

            if (campaign == null)
            {
                return NoContent();
            }

            var donation = new Donation
            {
                CampaignId = campaign.CampaignId,
                DonationId = donationId,
                DateDonated = DateTime.Now,
                IsConfirm = false,
                Amount = donationRequest.Amount,
                PaymentMethod = donationRequest.PaymentMethod
            };

            var url = _vnPayService.CreatePaymentUrl(new PaymentInformationModel
            {
                Amount = donationRequest.Amount,
                DonationId = donationId,
                CampaignCode = donationRequest.CampaignCode,
                OrderType = donationRequest.PaymentMethod,
                UserId = donation.UserId,
                CampaignId = donation.CampaignId
            }, HttpContext);

            if (string.IsNullOrEmpty(url))
            {
                return BadRequest("Unable to create VnPay payment.");
            }

            dbContext.Donations.Add(donation);
            await dbContext.SaveChangesAsync();

            return Ok(new { PaymentUrl = url });
        }

        [HttpGet("Execute-VnPay-Payment")]
        public async Task<IActionResult> ExecuteVnPayPayment()
        {
            var collections = Request.Query;
            var donationId = collections["vnp_TxnRef"].FirstOrDefault(); // Ensure it matches the query parameter name

            if (string.IsNullOrEmpty(donationId) || !Guid.TryParse(donationId, out Guid parsedDonationId))
            {
                return BadRequest("Invalid or missing donation ID.");
            }

            var donation = _vnPayService.PaymentExecute(collections);

            if (donation.DonationId != parsedDonationId)
            {
                return BadRequest("Donation ID mismatch.");
            }

            if (!donation.IsConfirm)
            {
                return BadRequest("Payment failed or was not confirmed.");
            }

            using (var transaction = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var existingDonation = await dbContext.Donations.FindAsync(parsedDonationId);
                    if (existingDonation == null)
                    {
                        return NotFound("Donation not found.");
                    }

                    existingDonation.IsConfirm = true;

                    // Update the current amount in the associated campaign
                    var campaign = await dbContext.Campaigns.FindAsync(existingDonation.CampaignId);
                    if (campaign != null)
                    {
                        campaign.CurrentAmount += existingDonation.Amount;
                        dbContext.Campaigns.Update(campaign);
                    }

                    dbContext.Donations.Update(existingDonation);
                    await dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                    //return Ok(mapper.Map<DonationDto>(existingDonation));
                    return Redirect($"https://localhost:4200/paymentsuccess?payment_method={existingDonation.PaymentMethod}&success=1&donation_id={existingDonation.DonationId}&amount={existingDonation.Amount}");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
                }
            }
        }


        // GET: api/NoUserDonation/ExecutePayment
        [HttpGet("Execute-Payment")]
        public async Task<IActionResult> ExecutePayment()
        {
            var collections = Request.Query;
            var donationId = collections["donation_id"].FirstOrDefault(); // Get DonationId from query

            if (string.IsNullOrEmpty(donationId) || !Guid.TryParse(donationId, out Guid parsedDonationId))
            {
                return BadRequest("Invalid or missing donation ID.");
            }

            var donation = payPalService.PaymentExecute(collections);
            if (donation.DonationId != parsedDonationId)
            {
                return BadRequest("Donation ID mismatch.");
            }

            if (!donation.IsConfirm)
            {
                return BadRequest("Payment failed or was not confirmed.");
            }

            using (var transaction = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var existingDonation = await dbContext.Donations.FindAsync(parsedDonationId);
                    if (existingDonation == null)
                    {
                        return NotFound("Donation not found.");
                    }

                    existingDonation.IsConfirm = true;

                    // Update the current amount in the associated campaign
                    var campaign = await dbContext.Campaigns.FindAsync(existingDonation.CampaignId);
                    if (campaign != null)
                    {
                        campaign.CurrentAmount += existingDonation.Amount;
                        dbContext.Campaigns.Update(campaign);
                    }

                    dbContext.Donations.Update(existingDonation);
                    await dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                    // Return the updated donation object as JSON
                    return Redirect($"https://localhost:4200/paymentsuccess?payment_method={existingDonation.PaymentMethod}&success=1&donation_id={existingDonation.DonationId}&amount={existingDonation.Amount}");
                    //return Ok(new
                    //{
                    //    existingDonation.CampaignId,
                    //    existingDonation.DonationId,
                    //    existingDonation.DateDonated,
                    //    existingDonation.IsConfirm,
                    //    existingDonation.UserId,
                    //    existingDonation.Amount,
                    //    existingDonation.PaymentMethod
                    //});
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
                }
            }
        }
    }
}