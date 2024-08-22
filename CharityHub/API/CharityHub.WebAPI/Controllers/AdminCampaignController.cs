using AutoMapper;
using CharityHub.Business.ViewModels;
using CharityHub.Data.Data;
using CharityHub.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CharityHub.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminCampaignController : ControllerBase
    {
        private readonly CharityHubDbContext dbContext;
        private readonly IMapper mapper;

        public AdminCampaignController(CharityHubDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        // POST: api/Campaign
        [HttpPost]
        public async Task<IActionResult> CreateCampaign([FromBody] AddCampaignRequestDto addCampaignRequestDto)
        {
            var campaign = mapper.Map<Campaign>(addCampaignRequestDto);

            await dbContext.Campaigns.AddAsync(campaign);
            await dbContext.SaveChangesAsync();

            return Ok(mapper.Map<CampaignDto>(campaign));
        }
    }
}
