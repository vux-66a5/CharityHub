using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CharityHub.Data.Models
{
    public class Campaign
    {
        [Required]
        [Key]
        public Guid CampaignId { get; set; }
        [Required]
        public int CampaignCode { get; set; }
        [Required]
        [MaxLength(100)]
        public string CampaignTitle { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(MAX)")]
        public string CampaignThumbnail { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string CampaignDescription { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TargetAmount { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal CurrentAmount { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        [MaxLength(100)]
        public string PartnerName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(MAX)")]
        public string PartnerLogo { get; set; }
        [Required]
        [MaxLength (11)]
        public string CampaignStatus { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(10)")]
        public string PartnerNumber { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }

        public IList<Donation> Donations { get; set; }
        public IList<UserFollows> UserFollows { get; set; }
        public IList<AdminAction> AdminActions { get; set; }
    }
}
