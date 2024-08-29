using AutoMapper;
using CharityHub.Business.ViewModels;
using CharityHub.Data.Data;
using CharityHub.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CharityHub.Business.Services.PayPalDonate
{
    public class NoUserDonationService : INoUserDonationService
    {
        private readonly CharityHubDbContext dbContext;
        private readonly IPayPalService payPalService;
        private readonly IMapper mapper;

        public NoUserDonationService(CharityHubDbContext dbContext, IPayPalService payPalService, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.payPalService = payPalService;
            this.mapper = mapper;
        }

        public async Task<string> CreatePayPalDonationAsync(AddDonationRequestDto donationRequest)
        {
            var donationId = Guid.NewGuid();

            var campaign = await dbContext.Campaigns
                .Where(c => c.CampaignCode == donationRequest.CampaignCode)
                .FirstOrDefaultAsync();

            if (campaign == null)
            {
                throw new InvalidOperationException("Khong ton tai Campaign.");
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

            var paymentUrl = await payPalService.CreatePaymentUrl(new PaymentInformation
            {
                Amount = donation.Amount,
                DonationId = donationId
            });

            if (string.IsNullOrEmpty(paymentUrl))
            {
                throw new InvalidOperationException("Khong the tao PayPal payment.");
            }

            dbContext.Donations.Add(donation);
            await dbContext.SaveChangesAsync();

            return paymentUrl;
        }

        public async Task<string> ExecutePaymentAsync(IQueryCollection query)
        {
            var donationId = query["donation_id"].FirstOrDefault();

            if (string.IsNullOrEmpty(donationId) || !Guid.TryParse(donationId, out Guid parsedDonationId))
            {
                throw new InvalidOperationException("Invalid or missing donation ID.");
            }

            var donation = payPalService.PaymentExecute(query);
            if (donation.DonationId != parsedDonationId)
            {
                throw new InvalidOperationException("Donation ID mismatch.");
            }

            if (!donation.IsConfirm)
            {
                throw new InvalidOperationException("Payment failed or was not confirmed.");
            }

            using (var transaction = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var existingDonation = await dbContext.Donations.FindAsync(parsedDonationId);
                    if (existingDonation == null)
                    {
                        throw new InvalidOperationException("Donation not found.");
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

                    // Return the success URL
                    return $"http://localhost:4200/paymentsuccess?payment_method={existingDonation.PaymentMethod}&success=1&donation_id={existingDonation.DonationId}&amount={existingDonation.Amount}";
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new InvalidOperationException($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}
