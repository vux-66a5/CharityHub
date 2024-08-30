using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharityHub.Data.Models
{
    public class Donation
    {
        [Required]
        [Key]
        public Guid DonationId { get; set; }
        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Amount { get; set; }
        [Required]
        [MaxLength(6)]
        public string PaymentMethod { get; set; }
        [Required]
        public bool IsConfirm { get; set; }
        [Required]
        public DateTime DateDonated { get; set; }

        public Guid? UserId { get; set; }
        public User User { get; set; }

        public Guid CampaignId { get; set; }
        public Campaign Campaign { get; set; }
    }
}
