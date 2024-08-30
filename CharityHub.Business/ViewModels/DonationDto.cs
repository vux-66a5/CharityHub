

using CharityHub.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CharityHub.Business.ViewModels
{
    public class DonationDto
    {
        public Guid DonationId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public bool IsConfirm { get; set; }
        public DateTime DateDonated { get; set; }

        public Guid? UserId { get; set; }

        public Guid CampaignId { get; set; }
    }
}
