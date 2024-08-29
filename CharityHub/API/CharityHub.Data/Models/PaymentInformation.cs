
namespace CharityHub.Data.Models
{
    public class PaymentInformation
    {
        public Decimal Amount { get; set; }
        public Guid DonationId { get; set; }
        public int CampaignCode { get; set; }
    }
}
