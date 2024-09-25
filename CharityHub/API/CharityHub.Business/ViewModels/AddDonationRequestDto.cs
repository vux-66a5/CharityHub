
namespace CharityHub.Business.ViewModels
{
    public class AddDonationRequestDto
    {
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public int CampaignCode { get; set; }
    }
}
