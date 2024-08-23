
using System.ComponentModel.DataAnnotations;

namespace CharityHub.Business.ViewModels
{
    public class RegisterRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]

        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string DisplayName { get; set; }
    }
}
