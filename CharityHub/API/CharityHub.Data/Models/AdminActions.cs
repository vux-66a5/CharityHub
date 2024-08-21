
using System.ComponentModel.DataAnnotations;

namespace CharityHub.Data.Models
{
    public class AdminActions
    {
        [Required]
        [Key]
        public Guid ActionId { get; set; }
        [Required]
        [MaxLength(15)]
        public string ActionType { get; set; }
        [Required]
        public DateTime CompletedAt { get; set; }

        public Guid AdminId { get; set; }
        public User Admin { get; set; }

        public Guid? TargetUserId { get; set; }
        public User TargetUser { get; set; }

        public Guid? TargetCampaignId { get; set; }
        public Campaign TargetCampaign { get; set; }
    }
}
