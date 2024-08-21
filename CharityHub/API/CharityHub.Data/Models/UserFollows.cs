using System.ComponentModel.DataAnnotations;

namespace CharityHub.Data.Models
{
    public class UserFollows
    {
        [Required]
        public bool IsNotified { get; set; }
        [Required]
        public DateTime DateFollowed { get; set; }

        [Key]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [Key]
        public Guid CampaignId { get; set; }
        public Campaign Campaign { get; set; }
    }
}
