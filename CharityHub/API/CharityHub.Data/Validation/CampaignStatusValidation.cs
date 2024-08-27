
using System.ComponentModel.DataAnnotations;

namespace CharityHub.Data.Validation
{
    public class CampaignStatusValidation : ValidationAttribute
    {
        private readonly List<string> _campaignStatus = new List<string>
        {
            "New",
            "Inprogress",
            "Closed"
        };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !_campaignStatus.Contains(value.ToString()))
            {
                return new ValidationResult($"Invalid question type. Valid types are: {string.Join(", ", _campaignStatus)}.");
            }

            return ValidationResult.Success;
        }
    }
}
