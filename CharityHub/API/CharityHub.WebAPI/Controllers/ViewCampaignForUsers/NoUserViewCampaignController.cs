using CharityHub.Business.Services.ViewCampaignService;
using Microsoft.AspNetCore.Mvc;

namespace CharityHub.WebAPI.Controllers.ViewCampaignForUsers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoUserViewCampaignController : ControllerBase
    {
        private readonly INoUserViewCampaignService noUserViewCampaignService;

        public NoUserViewCampaignController(INoUserViewCampaignService noUserViewCampaignService)
        {
            this.noUserViewCampaignService = noUserViewCampaignService;
        }

        // Danh sách đợt quyên góp dựa và trạng thái (status)
        [HttpGet("Campaign")]
        public async Task<IActionResult> GetCampaignsByStatus(string status)
        {
            var campaigns = await noUserViewCampaignService.GetCampaignsByStatusAsync(status);
            return Ok(campaigns);
        }

        // xem được danh sách những người đã quyên góp trên một đợt quyên góp
        [HttpGet("campaigns/{code}/donors")]
        public async Task<IActionResult> GetDonorsByCampaignCode(int code)
        {
            var donors = await noUserViewCampaignService.GetDonorsByCampaignCodeAsync(code);

            if (donors == null)
            {
                return NotFound("Campaign khong ton tai.");
            }

            return Ok(donors);
        }



        // Xem số tiền đã quyên góp và số tiền cần quyên góp 
        [HttpGet("campaigns/{code}/amounts")]
        public async Task<IActionResult> GetCampaignAmountsByCampaignCode(int code)
        {
            var campaignAmounts = await noUserViewCampaignService.GetCampaignAmountsByCampaignCodeAsync(code);

            if (campaignAmounts == null)
            {
                return NotFound();
            }

            return Ok(campaignAmounts);
        }
    }
}
