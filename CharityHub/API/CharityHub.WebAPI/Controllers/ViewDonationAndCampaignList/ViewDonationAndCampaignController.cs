using CharityHub.Business.Services.ViewDonationAndCampaignService;
using CharityHub.Business.ViewModels;
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
        private readonly IDonationService donationService;
        private readonly ICampaignService campaignService;
        private readonly CharityHubDbContext dbContext;

        public ViewDonationAndCampaignController(IDonationService donationService, ICampaignService campaignService, CharityHubDbContext dbContext)
        {
            this.donationService = donationService;
            this.campaignService = campaignService;
            this.dbContext = dbContext;
        }

        [HttpGet("Get-Donation-Details")]
        public async Task<IActionResult> GetDonationDetails()
        {
            var donationDetails = await donationService.GetDonationDetailsAsync();
            return Ok(donationDetails);
        }

        [HttpGet("Get-Donation-Details-By-DisplayName-And-CampaignCode")]
        public async Task<IActionResult> GetDonationDetailsByDisplayNameAndCampaignCode([FromQuery] string search = null)
        {
            var donationDetails = await donationService.GetDonationDetailsByDisplayNameAndCampaignCodeAsync(search);
            return Ok(donationDetails);
        }


        [HttpGet("Get-All-Campaigns")]
        public async Task<IActionResult> GetAllCampaigns()
        {
            var campaigns = await campaignService.GetAllCampaignsAsync();
            return Ok(campaigns);
        }

        [HttpGet("Get-Donations-By-CampaignId{campaignId}")]
        public List<DonationInfo> GetConfirmedDonationsByCampaignId(Guid campaignId)
        {
            var donations = dbContext.Donations
                .Where(d => d.CampaignId == campaignId && d.IsConfirm)
                .Select(d => new DonationInfo
                {
                    DisplayName = d.UserId == null ? "Nha hao tam" : d.User.DisplayName,
                    Amount = d.Amount
                })
                .ToList();

            return donations;
        }
    }
}
