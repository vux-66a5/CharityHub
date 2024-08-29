using AutoMapper;
using CharityHub.Business.ViewModels;
using CharityHub.Data.Data;
using CharityHub.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CharityHub.Business.Services.AdminCampaignService
{
    public class AdminCampaignService : IAdminCampaignService
    {
        private readonly CharityHubDbContext dbContext;
        private readonly IMapper mapper;

        public AdminCampaignService(CharityHubDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<List<CampaignViewCardDto>> GetCampaignsViewCardAsync()
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

            return campaigns;
        }

        public async Task<CampaignDto> CreateCampaignAsync(AddCampaignRequestDto addCampaignRequestDto)
        {
            var campaign = mapper.Map<Campaign>(addCampaignRequestDto);

            campaign.CampaignCode = CampaignCodeGenerator.GenerateUniqueCampaignCode(dbContext);
            campaign.DateCreated = DateTime.Now;
            campaign.CurrentAmount = 0;
            campaign.CampaignStatus = "New";

            await dbContext.Campaigns.AddAsync(campaign);
            await dbContext.SaveChangesAsync();

            return mapper.Map<CampaignDto>(campaign);
        }

        public async Task<string> DeleteCampaignAsync(Guid campaignId)
        {
            var campaign = await dbContext.Campaigns
            .FirstOrDefaultAsync(c => c.CampaignId == campaignId);
            if (campaign == null)
            {
                return "Campaign khong ton tai!";
            }

            dbContext.Campaigns.Remove(campaign);
            await dbContext.SaveChangesAsync();

            return "Campaign da duoc xoa thanh cong!";
        }

        public async Task<string> DeleteNewCampaignAsync(int campaignCode)
        {
            var campaign = await dbContext.Campaigns
                .FirstOrDefaultAsync(c => c.CampaignCode == campaignCode && c.CampaignStatus.ToLower() == "New".ToLower());
            if (campaign == null)
            {
                return "Campaign khong ton tai!";
            }

            dbContext.Campaigns.Remove(campaign);
            await dbContext.SaveChangesAsync();

            return "Campaign da duoc xoa thanh cong!";
        }

        public async Task<string> ExtendCampaignEndDateAsync(int campaignCode, DateTime newEndDate)
        {
            var campaign = await dbContext.Campaigns
                .FirstOrDefaultAsync(c => c.CampaignCode == campaignCode);

            if (campaign == null)
            {
                return "Campaign khong ton tai.";
            }

            if (newEndDate <= campaign.EndDate)
            {
                return "Campaign phai co EndDate moi muon hon EndDate hien tai.";
            }

            campaign.EndDate = newEndDate;
            await dbContext.SaveChangesAsync();

            return "Campaign end date da duoc mo rong thanh cong.";
        }

        public async Task<List<Campaign>> GetAllCampaignAsync()
        {
            var campaigns = await dbContext.Campaigns.ToListAsync();
            return campaigns;
        }

        public async Task<object> GetDonationProgressAsync(int campaignCode)
        {
            var campaign = await dbContext.Campaigns
                .Where(c => c.CampaignCode == campaignCode)
                .Select(c => new
                {
                    c.TargetAmount,
                    c.CurrentAmount
                })
                .FirstOrDefaultAsync();

            return campaign;
        }

        public async Task<IEnumerable<CampaignDto>> SearchCampaignsByCodeAsync(int campaignCode)
        {
            var campaigns = await dbContext.Campaigns
                .Where(c => c.CampaignCode == campaignCode)
                .ToListAsync();

            return mapper.Map<IEnumerable<CampaignDto>>(campaigns);
        }

        public async Task<IEnumerable<CampaignDto>> SearchCampaignsByPhoneAsync(string phoneNumber)
        {
            var campaigns = await dbContext.Campaigns
                .Where(c => c.PartnerNumber == phoneNumber)
                .ToListAsync();

            return mapper.Map<IEnumerable<CampaignDto>>(campaigns);
        }

        public async Task<IEnumerable<CampaignDto>> SearchCampaignsByStatusAsync(string status)
        {
            var campaigns = await dbContext.Campaigns
                .Where(c => c.CampaignStatus == status)
                .ToListAsync();

            return mapper.Map<IEnumerable<CampaignDto>>(campaigns);
        }

        public async Task<CampaignDto> UpdateCampaignAsync(Guid campaignId, UpdateCampaignRequestDto updatedCampaign)
        {
            var campaignModel = mapper.Map<Campaign>(updatedCampaign);

            var existingCampaign = await dbContext.Campaigns
                .FirstOrDefaultAsync(c => c.CampaignId == campaignId);

            if (existingCampaign == null) return null;

            if (existingCampaign.CurrentAmount > 0)
                return null;

            existingCampaign.CampaignTitle = campaignModel.CampaignTitle;
            existingCampaign.CampaignDescription = campaignModel.CampaignDescription;
            existingCampaign.CampaignThumbnail = campaignModel.CampaignThumbnail;
            existingCampaign.PartnerName = campaignModel.PartnerName;
            existingCampaign.PartnerNumber = campaignModel.PartnerNumber;
            existingCampaign.PartnerLogo = campaignModel.PartnerLogo;

            await dbContext.SaveChangesAsync();
            return mapper.Map<CampaignDto>(existingCampaign);
        }

        public async Task<CampaignDto> UpdateStartAndEndDateAsync(Guid campaignId, StartAndEndDateCampaign startAndEndDateCampaign)
        {
            var campaign = await dbContext.Campaigns
                .FirstOrDefaultAsync(c => c.CampaignId == campaignId);

            if (campaign == null)
            {
                return null;
            }

            if (campaign.StartDate != null || campaign.EndDate != null || campaign.CampaignStatus != "New")
            {
                return null;
            }

            campaign.StartDate = startAndEndDateCampaign.StartDate;
            campaign.EndDate = startAndEndDateCampaign.EndDate;

            dbContext.Campaigns.Update(campaign);
            await dbContext.SaveChangesAsync();

            return mapper.Map<CampaignDto>(campaign);
        }

        public async Task<List<CampaignDto>> SearchCampaignsAsync(string query)
        {
            // Xây dựng truy vấn cơ sở dữ liệu
            var queryable = dbContext.Campaigns.AsQueryable();

            // Nếu chuỗi tìm kiếm không có giá trị, trả về toàn bộ danh sách
            if (string.IsNullOrEmpty(query))
            {
                return mapper.Map<List<CampaignDto>>(await dbContext.Campaigns.ToListAsync());
            }

            // Tách chuỗi tìm kiếm thành các từ khóa
            var keywords = query.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // Áp dụng bộ lọc cho từng từ khóa
            foreach (var keyword in keywords)
            {
                // Lưu ý: Cần kiểm tra keyword không phải là null
                if (!string.IsNullOrEmpty(keyword))
                {
                    queryable = queryable
                        .Where(c => c.CampaignCode.ToString().Contains(keyword) ||
                                    c.CampaignTitle.Contains(keyword) ||
                                    c.PartnerName.Contains(keyword) ||
                                    c.PartnerNumber.Contains(keyword) ||
                                    c.CampaignStatus.Contains(keyword));
                }
            }

            // Lấy danh sách chiến dịch theo truy vấn đã lọc
            var campaigns = await queryable.ToListAsync();

            // Chuyển đổi các đối tượng Campaign thành CampaignDto
            return mapper.Map<List<CampaignDto>>(campaigns);
        }

    }

}
