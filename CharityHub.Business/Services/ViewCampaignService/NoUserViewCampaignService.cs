using AutoMapper;
using CharityHub.Business.ViewModels;
using CharityHub.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace CharityHub.Business.Services.ViewCampaignService
{
    public class NoUserViewCampaignService : INoUserViewCampaignService
    {
        private readonly CharityHubDbContext dbContext;
        private readonly IMapper mapper;

        public NoUserViewCampaignService(CharityHubDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<CampaignAmountsDto> GetCampaignAmountsByCampaignCodeAsync(int code)
        {
            var campaign = await dbContext.Campaigns
                .Where(c => c.CampaignCode == code)
                .Select(c => new CampaignAmountsDto
                {
                    TargetAmount = c.TargetAmount,
                    CurrentAmount = c.CurrentAmount
                }).FirstOrDefaultAsync();

            return campaign;
        }

        public async Task<List<CampaignDto>> GetCampaignsByStatusAsync(string status)
        {
            var campaigns = await dbContext.Campaigns
                .Where(c => c.CampaignStatus.ToLower() == status.ToLower())
                .ToListAsync();

            return mapper.Map<List<CampaignDto>>(campaigns);
        }

        public async Task<List<DonorDto>> GetDonorsByCampaignCodeAsync(int code)
        {
            var campaign = await dbContext.Campaigns
                .Where(c => c.CampaignCode == code)
                .FirstOrDefaultAsync();

            if (campaign == null)
            {
                return null;
            }

            var donors = await(from d in dbContext.Donations
                               join u in dbContext.Users on d.UserId equals u.Id into userGroup
                               from u in userGroup.DefaultIfEmpty()
                               where d.CampaignId == campaign.CampaignId && d.IsConfirm
                               orderby d.Amount descending
                               select new DonorDto
                               {
                                   DisplayName = u != null ? u.DisplayName : "Nhà hảo tâm ẩn danh",
                                   Amount = d.Amount
                               }).ToListAsync();

            return donors;
        }
    }
}
