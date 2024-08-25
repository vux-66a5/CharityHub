using CharityHub.Business.ViewModels;
using CharityHub.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace CharityHub.Business.Services.ViewDonationAndCampaignService
{
    public class DonationService : IDonationService
    {
        private readonly CharityHubDbContext dbContext;

        public DonationService(CharityHubDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<DonationDetailsDto>> GetDonationDetailsAsync()
        {
            var donationDetails = await (from d in dbContext.Donations
                                        join u in dbContext.Users on d.UserId equals u.Id into userGroup
                                        from u in userGroup.DefaultIfEmpty()
                                        join c in dbContext.Campaigns on d.CampaignId equals c.CampaignId
                                        orderby d.Amount descending
                                        select new DonationDetailsDto
                                        {
                                            DisplayName = u != null ? u.DisplayName : "Nha hao tam",
                                            Amount = d.Amount,
                                            CampaignCode = c.CampaignCode,
                                            CampaignTitle = c.CampaignTitle,
                                            DateDonated = d.DateDonated
                                        }).ToListAsync();

            return donationDetails;
        }

        public async Task<List<DonationDetailsDto>> GetDonationDetailsByDisplayNameAndCampaignCodeAsync(string search)
        {
            var donationDetails = await (from d in dbContext.Donations
                                        join u in dbContext.Users on d.UserId equals u.Id into userGroup
                                        from u in userGroup.DefaultIfEmpty()
                                        join c in dbContext.Campaigns on d.CampaignId equals c.CampaignId
                                        where string.IsNullOrEmpty(search) ||
                                              c.CampaignCode.ToString().Contains(search) ||
                                              (u != null && u.DisplayName.Contains(search)) ||
                                              ("Nha hao tam".Contains(search) && u == null)
                                        orderby d.Amount descending
                                        select new DonationDetailsDto
                                        {
                                            DisplayName = u != null ? u.DisplayName : "Nha hao tam",
                                            Amount = d.Amount,
                                            CampaignCode = c.CampaignCode,
                                            CampaignTitle = c.CampaignTitle,
                                            DateDonated = d.DateDonated
                                        }).ToListAsync();

            return donationDetails;
        }
    }
}
