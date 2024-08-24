using AutoMapper;
using CharityHub.Business.Services;
using CharityHub.Business.ViewModels;
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

        public NoUserDonationController(CharityHubDbContext dbContext, IPayPalService payPalService, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.payPalService = payPalService;
            this.mapper = mapper;
        }

        // POST: api/NoUserDonation/paypal/create
        [HttpPost("paypal/create")]
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


        // GET: api/NoUserDonation/ExecutePayment
        [HttpGet("ExecutePayment")]
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
                    return Ok(mapper.Map<DonationDto>(existingDonation));
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
