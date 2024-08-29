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

        public ViewDonationAndCampaignController(IDonationService donationService, ICampaignService campaignService)
        {
            this.donationService = donationService;
            this.campaignService = campaignService;
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
        public async Task<ActionResult<List<DonationInfo>>> GetConfirmedDonationsByCampaignId(Guid campaignId)
        {
            var donations = await campaignService.GetConfirmedDonationsByCampaignIdAsync(campaignId);
            return Ok(donations);
        }

        [HttpGet("GetAllCampaignsExceptNew")]
        public async Task<IActionResult> GetAllCampaignExceptNew()
        {
            var campaigns = await campaignService.GetAllCampaignsExceptNewAsync();
            return Ok(campaigns);
        }

        [HttpGet("GetCampaignCodeByDonationId/{donationId}")]
        public async Task<IActionResult> GetCampaignCodeByDonationId(Guid donationId)
        {
            int campaignCode = await campaignService.GetCampaignCodeByDonationIdAsync(donationId);
            return Ok(new { campaignCode }); // Trả về đối tượng với thuộc tính campaignCode
        }

        [HttpGet("GetCampaignStatus/{campaignId}")]
        public async Task<IActionResult> GetCampaignStatus(Guid campaignId)
        {
            string campaignStatus = await campaignService.GetCampaignStatusAsync(campaignId);
            return Ok(new { campaignStatus });
        }
    }
}
