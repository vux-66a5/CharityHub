

namespace CharityHub.Business.ViewModels
{
    public class AddCampaignRequestDto
    {
        public int CampaignCode { get; set; }
        public string CampaignTitle { get; set; }
        public string CampaignThumbnail { get; set; }
        public string CampaignDescription { get; set; }
        public decimal TargetAmount { get; set; } = 0;
        public decimal CurrentAmount { get; set; }
        public string PartnerName { get; set; }
        public string PartnerLogo { get; set; }
        public string CampaignStatus { get; set; }
        public string PartnerNumber { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
