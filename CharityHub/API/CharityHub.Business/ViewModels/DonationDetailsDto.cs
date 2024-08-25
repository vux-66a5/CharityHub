

namespace CharityHub.Business.ViewModels
{
    public class DonationDetailsDto
    {
        public string DisplayName { get; set; }
        public decimal Amount { get; set; }
        public int CampaignCode { get; set; }
        public string CampaignTitle { get; set; }
        public DateTime DateDonated { get; set; }
    }
}
