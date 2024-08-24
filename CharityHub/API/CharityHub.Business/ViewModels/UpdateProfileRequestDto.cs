using Newtonsoft.Json;

namespace CharityHub.Business.ViewModels
{
    public class UpdateProfileRequestDto
    {
        public string? DisplayName { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
