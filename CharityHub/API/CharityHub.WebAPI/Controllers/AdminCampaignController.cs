using AutoMapper;
using CharityHub.Business;
using CharityHub.Business.Services.AdminCampaignService;
using CharityHub.Business.Services.ViewDonationAndCampaignService;
using CharityHub.Business.ViewModels;
using CharityHub.Data.Data;
using CharityHub.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CharityHub.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminCampaignController : ControllerBase
    {
        private readonly IAdminCampaignService adminCampaignService;
        private readonly CharityHubDbContext dbContext;

        public AdminCampaignController(IAdminCampaignService adminCampaignService, CharityHubDbContext dbContext)
        {
            this.adminCampaignService = adminCampaignService;
            this.dbContext = dbContext;
        }

        [HttpGet("Get-Campaigns-View-Card")]
        public async Task<IActionResult> GetCampaignsViewCard()
        {
            var campaigns = await dbContext.Campaigns
                .Select(c => new CampaignViewCardDto
                {
                    CampaignId = c.CampaignId,
                    CampaignCode = c.CampaignCode,
                    CampaignTitle = c.CampaignTitle,
                    PartnerName = c.PartnerName,
                    CampaignStatus = c.CampaignStatus,
                    CampaignDescription = c.CampaignDescription,
                    TargetAmount = c.TargetAmount,
                    CurrentAmount = c.CurrentAmount,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    PartnerNumber = c.PartnerNumber
                })
                .ToListAsync();
            return Ok(campaigns);
        }

        // POST: api/Campaign
        [HttpPost("Create-Campaign")]
        public async Task<IActionResult> CreateCampaign([FromBody] AddCampaignRequestDto addCampaignRequestDto)
        {
            var result = await adminCampaignService.CreateCampaignAsync(addCampaignRequestDto);
            return Ok(result);
        }

        // GET: api/Campaign/search?status=Active
        [HttpGet("search-By-Status")]
        public async Task<IActionResult> SearchCapaigns(string status)
        {
            var result = await adminCampaignService.SearchCampaignsByStatusAsync(status);
            return Ok(result);
        }

        // GET: api/Campaign/searchByPhone?phoneNumber=123456789
        [HttpGet("Search-Campaign-ByPhone")]
        public async Task<IActionResult> SearchCampaignByPhone(string phoneNumber)
        {
            var result = await adminCampaignService.SearchCampaignsByPhoneAsync(phoneNumber);
            return Ok(result);
        }

        // GET: api/Campaign/searchByCode
        [HttpGet("Search-Campaign-By-Code")]
        public async Task<IActionResult> SearchCampaignByCode(int campaignCode)
        {
            var result = await adminCampaignService.SearchCampaignsByCodeAsync(campaignCode);
            return Ok(result);
        }

        // DELETE: api/Campaign/{campaignCode}/DeleteNewCampaign
        [HttpDelete("Delete-New-Campaign/{campaignCode}")]
        public async Task<IActionResult> DeleteNewCampaign(int campaignCode)
        {
            var result = await adminCampaignService.DeleteNewCampaignAsync(campaignCode);
            return result == "Campaign khong ton tai!" ? NotFound(result) : Ok(result);
        }


        // DELETE: api/Campaign/{campaignCode}/DeleteCampaign
        [HttpDelete("Delete-Campaign/{campaignId}")]
        public async Task<IActionResult> DeleteCampaign(Guid campaignId)
        {
            var result = await adminCampaignService.DeleteCampaignAsync(campaignId);
            return result == "Campaign khong ton tai!" ? NotFound(result) : Ok(result);
        }

        // PUT: api/Campaign/{campaignCode}
        [HttpPut("Update-Campaign/{campaignId}")]
        public async Task<IActionResult> UpdateCampaign(Guid campaignId, UpdateCampaignRequestDto updatedCampaign)
        {
            var result = await adminCampaignService.UpdateCampaignAsync(campaignId, updatedCampaign);
            return result == null ? NotFound("Campaign khong ton tai.") : Ok(result);
        }

        // GET: api/Campaign/{campaignCode}/progress
        [HttpGet("Get-Donation-Progress/{campaignCode}")]
        public async Task<IActionResult> GetDonationProgress(int campaignCode)
        {
            var result = await adminCampaignService.GetDonationProgressAsync(campaignCode);
            return result == null ? NotFound("Campaign khong ton tai.") : Ok(result);
        }

        // PUT: api/Campaign/{campaignCode}/extend
        [HttpPut("Extend-Campaign-EndDate/{campaignCode}")]
        public async Task<IActionResult> ExtendCampaignEndDate(int campaignCode, [FromBody] DateTime newEndDate)
        {
            var result = await adminCampaignService.ExtendCampaignEndDateAsync(campaignCode, newEndDate);
            return result.StartsWith("Campaign") ? Ok(result) : BadRequest(result);
        }

        // cập nhật thời gian bắt đầu vào thời gian kết thúc chiến dịch cho chiến dịch chưa bắt đầu (StartDate và EndDate = null)
        [HttpPut("Update-Start-And-End-Date/{campaignId}")]
        public async Task<IActionResult> UpdateStartAndEndDate(Guid campaignId, [FromBody] StartAndEndDateCampaign startAndEndDateCampaign)
        {
            var result = await adminCampaignService.UpdateStartAndEndDateAsync(campaignId, startAndEndDateCampaign);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("Search-Campaigns-By-Code-Status-Name-Phone")]
        public async Task<IActionResult> SearchCampaignsByCodeStatusNamePhone(
            [FromQuery] string? query)
        {
            var result = await adminCampaignService.SearchCampaignsAsync(query);
            return Ok(result);
        }

    }
}