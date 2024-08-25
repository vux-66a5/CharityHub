﻿
namespace CharityHub.Business.ViewModels
{
    public class UpdateCampaignRequestDto
    {
        public string CampaignTitle { get; set; }
        public string CampaignThumbnail { get; set; }
        public string CampaignDescription { get; set; }
        public decimal TargetAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string PartnerName { get; set; }
        public string PartnerLogo { get; set; }
        public string CampaignStatus { get; set; }
        public string PartnerNumber { get; set; }
    }
}
