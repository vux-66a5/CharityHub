//using AutoMapper;
//using CharityHub.Business.ViewModels;
//using CharityHub.Data.Data;
//using CharityHub.Data.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace CharityHub.Business.Services.PayPalDonate
//{
//    public class UserDonationService : IUserDonationService
//    {
//        private readonly CharityHubDbContext dbContext;
//        private readonly IPayPalService payPalService;
//        private readonly IMapper mapper;

//        public UserDonationService(CharityHubDbContext dbContext, IPayPalService payPalService, IMapper mapper)
//        {
//            this.dbContext = dbContext;
//            this.payPalService = payPalService;
//            this.mapper = mapper;
//        }

//        public async Task<string> CreatePayPalDonationAsync(AddDonationRequestDto donationRequest, Guid userId)
//        {
//            var donationId = Guid.NewGuid();

//            var campaign = await dbContext.Campaigns
//                .Where(c => c.CampaignCode == donationRequest.CampaignCode)
//                .FirstOrDefaultAsync();

//            if (campaign == null)
//            {
//                throw new InvalidOperationException("Campaign khong ton tai.");
//            }

//            var donation = new Donation
//            {
//                CampaignId = campaign.CampaignId,
//                DonationId = donationId,
//                DateDonated = DateTime.Now,
//                IsConfirm = false,
//                Amount = donationRequest.Amount,
//                PaymentMethod = donationRequest.PaymentMethod,
//                UserId = userId
//            };

//            var paymentUrl = await payPalService.CreatePaymentUrl(new PaymentInformation
//            {
//                Amount = donation.Amount,
//                DonationId = donationId // Pass DonationId to PayPal
//            });

//            if (string.IsNullOrEmpty(paymentUrl))
//            {
//                throw new InvalidOperationException("khong the tao PayPal payment.");
//            }

//            dbContext.Donations.Add(donation);
//            await dbContext.SaveChangesAsync();

//            return paymentUrl;
//        }

//        public async Task<IActionResult> ExecutePaymentAsync(IQueryCollection query, Guid userId)
//        {
//            var donationId = query["donation_id"].FirstOrDefault();

//            if (string.IsNullOrEmpty(donationId) || !Guid.TryParse(donationId, out Guid parsedDonationId))
//            {
//                return new BadRequestObjectResult("Invalid or missing donation ID.");
//            }

//            var donation = payPalService.PaymentExecute(query);
//            if (donation.DonationId != parsedDonationId)
//            {
//                return new BadRequestObjectResult("Donation ID mismatch.");
//            }

//            if (!donation.IsConfirm)
//            {
//                return new BadRequestObjectResult("Payment failed or was not confirmed.");
//            }

//            using (var transaction = await dbContext.Database.BeginTransactionAsync())
//            {
//                try
//                {
//                    var existingDonation = await dbContext.Donations.FindAsync(parsedDonationId);
//                    if (existingDonation == null)
//                    {
//                        return new NotFoundObjectResult("Donation khong ton tai.");
//                    }

//                    existingDonation.IsConfirm = true;

//                    // Update the current amount in the associated campaign
//                    var campaign = await dbContext.Campaigns.FindAsync(existingDonation.CampaignId);
//                    if (campaign != null)
//                    {
//                        campaign.CurrentAmount += existingDonation.Amount;
//                        dbContext.Campaigns.Update(campaign);
//                    }

//                    dbContext.Donations.Update(existingDonation);
//                    await dbContext.SaveChangesAsync();
//                    await transaction.CommitAsync();

//                    // Return the updated donation object as JSON
//                    return new OkObjectResult(mapper.Map<DonationDto>(existingDonation));
//                }
//                catch (Exception ex)
//                {
//                    await transaction.RollbackAsync();
//                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
//                }
//            }
//        }
//    }
//}
