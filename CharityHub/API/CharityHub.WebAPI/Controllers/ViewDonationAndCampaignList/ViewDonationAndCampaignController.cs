using CharityHub.Data.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CharityHub.WebAPI.Controllers.ViewDonationList
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewDonationAndCampaignController : ControllerBase
    {
        private readonly CharityHubDbContext dbContext;

        public ViewDonationAndCampaignController(CharityHubDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("GetAllDonations/details")]
        public async Task<IActionResult> GetDonationDetails()
        {
            var donationDetails = await (from d in dbContext.Donations
                                         join u in dbContext.Users on d.UserId equals u.Id into userGroup
                                         from u in userGroup.DefaultIfEmpty()
                                         join c in dbContext.Campaigns on d.CampaignId equals c.CampaignId
                                         orderby d.Amount descending
                                         select new
                                         {
                                             DisplayName = u != null ? u.DisplayName : "Nha hao tam",
                                             d.Amount,
                                             c.CampaignCode,
                                             c.CampaignTitle,
                                             d.DateDonated
                                         }).ToListAsync();

            return Ok(donationDetails);
        }

        [HttpGet("SearchAllDonations/details")]
        public async Task<IActionResult> GetDonationDetailsByDisplayNameAndCampaignCode([FromQuery] string search = null)
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
                                         select new
                                         {
                                             DisplayName = u != null ? u.DisplayName : "Nha hao tam",
                                             d.Amount,
                                             c.CampaignCode,
                                             c.CampaignTitle,
                                             d.DateDonated
                                         }).ToListAsync();

            return Ok(donationDetails);
        }



        [HttpGet("GetAllCampaigns")]
        public async Task<IActionResult> GetAllCampaigns()
        {
            var campaigns = await dbContext.Campaigns
                .Select(c => new
                {
                    c.CampaignTitle,
                    c.CampaignCode,
                    c.CampaignThumbnail,
                    c.CampaignDescription,
                    c.TargetAmount,
                    c.CurrentAmount,
                    c.PartnerLogo,
                    c.PartnerName,
                    c.EndDate,
                    c.StartDate,
                    ConfirmedDonationCount = c.Donations.Count(d => d.IsConfirm)
                })
                .ToListAsync();

            return Ok(campaigns);
        }
    }
}
