

namespace CharityHub.Business.ViewModels
{
    public class AddCampaignRequestDto
    {
        public string CampaignTitle { get; set; }
        public string CampaignThumbnail { get; set; }
        public string CampaignDescription { get; set; }
        public decimal TargetAmount { get; set; }
        public string PartnerName { get; set; }
        public string PartnerLogo { get; set; }
        public string CampaignStatus { get; set; }
        public string PartnerNumber { get; set; }
    }
}
