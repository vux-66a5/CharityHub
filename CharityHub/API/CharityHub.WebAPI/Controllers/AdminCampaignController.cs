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

        public AdminCampaignController(IAdminCampaignService adminCampaignService)
        {
            this.adminCampaignService = adminCampaignService;
        }

        // POST: api/Campaign
        [HttpPost]
        public async Task<IActionResult> CreateCampaign([FromBody] AddCampaignRequestDto addCampaignRequestDto)
        {
            var result = await adminCampaignService.CreateCampaignAsync(addCampaignRequestDto);
            return Ok(result);
        }

        // GET: api/Campaign/search?status=Active
        [HttpGet("searchByStatus")]
        public async Task<IActionResult> SearchCapaigns(string status)
        {
            var result = await adminCampaignService.SearchCampaignsByStatusAsync(status);
            return Ok(result);
        }

        // GET: api/Campaign/searchByPhone?phoneNumber=123456789
        [HttpGet("searchByPhone")]
        public async Task<IActionResult> SearchCampaignByPhone(string phoneNumber)
        {
            var result = await adminCampaignService.SearchCampaignsByPhoneAsync(phoneNumber);
            return Ok(result);
        }

        // GET: api/Campaign/searchByCode
        [HttpGet("searchByCode")]
        public async Task<IActionResult> SearchCampaignByCode(int campaignCode)
        {
            var result = await adminCampaignService.SearchCampaignsByCodeAsync(campaignCode);
            return Ok(result);
        }

        // DELETE: api/Campaign/{campaignCode}/DeleteNewCampaign
        [HttpDelete("{campaignCode}/DeleteNewCampaign")]
        public async Task<IActionResult> DeleteNewCampaign(int campaignCode)
        {
            var result = await adminCampaignService.DeleteNewCampaignAsync(campaignCode);
            return result == "Campaign khong ton tai!" ? NotFound(result) : Ok(result);
        }


        // DELETE: api/Campaign/{campaignCode}/DeleteCampaign
        [HttpDelete("{campaignCode}/DeleteCampaign")]
        public async Task<IActionResult> DeleteCampaign(int campaignCode)
        {
            var result = await adminCampaignService.DeleteCampaignAsync(campaignCode);
            return result == "Campaign khong ton tai!" ? NotFound(result) : Ok(result);
        }

        // PUT: api/Campaign/{campaignCode}
        [HttpPut("{campaignCode}")]
        public async Task<IActionResult> UpdateCampaign(int campaignCode, UpdateCampaignRequestDto updatedCampaign)
        {
            var result = await adminCampaignService.UpdateCampaignAsync(campaignCode, updatedCampaign);
            return result == null ? NotFound("Campaign not found.") : Ok(result);
        }

        // GET: api/Campaign/{campaignCode}/progress
        [HttpGet("{campaignCode}/progress")]
        public async Task<IActionResult> GetDonationProgress(int campaignCode)
        {
            var result = await adminCampaignService.GetDonationProgressAsync(campaignCode);
            return result == null ? NotFound("Campaign khong ton tai.") : Ok(result);
        }

        // PUT: api/Campaign/{campaignCode}/extend
        [HttpPut("{campaignCode}/extend")]
        public async Task<IActionResult> ExtendCampaignEndDate(int campaignCode, [FromBody] DateTime newEndDate)
        {
            var result = await adminCampaignService.ExtendCampaignEndDateAsync(campaignCode, newEndDate);
            return result.StartsWith("Campaign") ? Ok(result) : BadRequest(result);
        }

        // cập nhật thời gian bắt đầu vào thời gian kết thúc chiến dịch cho chiến dịch chưa bắt đầu (StartDate và EndDate = null)
        [HttpPut("{campaignCode}/UpdateStartAndDate")]
        public async Task<IActionResult> UpdateStartAndDate(int campaignCode, [FromBody] StartAndEndDateCampaign startAndEndDateCampaign)
        {
            var result = await adminCampaignService.UpdateStartAndEndDateAsync(campaignCode, startAndEndDateCampaign);
            return result == null ? NotFound() : Ok(result);
        }
    }
}
