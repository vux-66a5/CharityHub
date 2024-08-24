
namespace CharityHub.Business.ViewModels
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string  Email { get; set; }
        public string DisplayName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastLoginDate { get; set; }
    }
}
