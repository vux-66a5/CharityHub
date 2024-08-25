using CharityHub.Business.ViewModels;
using CharityHub.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharityHub.Business.Services.ViewCampaignService
{
    public class UserViewCampaignService : IUserViewCampaignService
    {
        private readonly CharityHubDbContext dbContext;

        public UserViewCampaignService(CharityHubDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<DonatedByMyselfDto>> GetUserDonationsAsync(Guid userId)
        {
            var donations = await(from d in dbContext.Donations
                                  join c in dbContext.Campaigns on d.CampaignId equals c.CampaignId
                                  where d.UserId == userId
                                  select new DonatedByMyselfDto
                                  {
                                      CampaignTitle = c.CampaignTitle,
                                      Amount = d.Amount,
                                      DateDonated = d.DateDonated
                                  }).ToListAsync();

            return donations;
        }
    }
}
