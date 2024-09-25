
namespace CharityHub.Business.VNPay.Models
{
    public class PaymentInformationModel
    {
        public string OrderType { get; set; }
        public Decimal Amount { get; set; }
        public Guid DonationId { get; set; }
        public Guid CampaignId { get; set; }
        public int CampaignCode { get; set; }
        public Guid? UserId { get; set; }
    }
}
