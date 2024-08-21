using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharityHub.Data.Models
{
    public class User: IdentityUser<Guid>
    {
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public DateTime LastLoginDate { get; set; }   

        public IList<Donation> Donations { get; set; }
        public IList<UserFollows> UserFollows { get; set; }
        public IList<AdminActions> AdminActions { get; set; }
    }
}
