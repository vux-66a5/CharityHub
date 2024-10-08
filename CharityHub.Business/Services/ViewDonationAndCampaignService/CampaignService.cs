﻿

using CharityHub.Business.ViewModels;
using CharityHub.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CharityHub.Business.Services.ViewDonationAndCampaignService
{
    public class CampaignService : ICampaignService
    {
        private readonly CharityHubDbContext dbContext;

        public CampaignService(CharityHubDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<DonorDto>> GetConfirmedDonationsByCampaignCodeAsync(int code)
        {
            var campaign = await dbContext.Campaigns
                .Where(c => c.CampaignCode == code)
                .FirstOrDefaultAsync();

            if (campaign == null)
            {
                return null;
            }

            var donors = await (from d in dbContext.Donations
                                join u in dbContext.Users on d.UserId equals u.Id into userGroup
                                from u in userGroup.DefaultIfEmpty()
                                where d.CampaignId == campaign.CampaignId && d.IsConfirm
                                orderby d.DateDonated descending
                                select new DonorDto
                                {
                                    DisplayName = u != null ? u.DisplayName : "Nhà hảo tâm ẩn danh",
                                    Amount = d.Amount
                                }).ToListAsync();

            return donors;
        }

        public async Task<List<dynamic>> GetAllCampaignsExceptNewAsync()
        {
            var campaigns = await dbContext.Campaigns
                .Where(c => c.CampaignStatus != "New") // Filter out campaigns with status "New"
                .Select(c => new
                {
                    c.CampaignTitle,
                    c.CampaignCode,
                    c.CampaignThumbnail,
                    c.CampaignDescription,
                    c.TargetAmount,
                    c.CurrentAmount,
                    c.PartnerLogo,
                    c.CampaignStatus,
                    c.PartnerName,
                    c.EndDate,
                    c.StartDate,
                    ConfirmedDonationCount = c.Donations.Count(d => d.IsConfirm)
                })
                .ToListAsync();

            return campaigns.Cast<dynamic>().ToList();
        }

        public async Task<List<CampaignCardDto>> GetAllCampaignsAsync()
        {
            var campaigns = await dbContext.Campaigns
                .Select(c => new CampaignCardDto
                {
                    CampaignTitle = c.CampaignTitle,
                    CampaignCode = c.CampaignCode,
                    CampaignThumbnail = c.CampaignThumbnail,
                    CampaignDescription = c.CampaignDescription,
                    TargetAmount = c.TargetAmount,
                    CurrentAmount = c.CurrentAmount,
                    PartnerLogo = c.PartnerLogo,
                    PartnerName = c.PartnerName,
                    EndDate = c.EndDate,
                    StartDate = c.StartDate,
                    ConfirmedDonationCount = c.Donations.Count(d => d.IsConfirm)
                })
                .ToListAsync();

            return campaigns;
        }

        public async Task<int> GetCampaignCodeByDonationIdAsync(Guid donationId)
        {
            // Find the donation with the specified donationId
            var campaignId = await dbContext.Donations
                .Where(d => d.DonationId == donationId)
                .Select(d => d.CampaignId)
                .FirstOrDefaultAsync();

            if (campaignId == Guid.Empty)
            {
                // Return a meaningful message or handle the "not found" case as needed
                return -1; // Or throw an exception, or return a specific message
            }

            // Find the campaign with the specified CampaignId
            var campaignCode = await dbContext.Campaigns
                .Where(c => c.CampaignId == campaignId)
                .Select(c => c.CampaignCode)
                .FirstOrDefaultAsync();

            return campaignCode;
        }

        public async Task<string> GetCampaignStatusAsync(Guid campaignId)
        {
            var campaign = await dbContext.Campaigns
                .Where(c => c.CampaignId == campaignId)
                .Select(c => c.CampaignStatus)
                .FirstOrDefaultAsync();

            if (campaign == null)
            {
                return "Campaign not found.";
            }

            return campaign;
        }

    }
}