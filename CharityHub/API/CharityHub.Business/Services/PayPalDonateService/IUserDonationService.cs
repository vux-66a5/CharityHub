using CharityHub.Business.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CharityHub.Business.Services.PayPalDonate
{
    public interface IUserDonationService
    {
        Task<string> CreatePayPalDonationAsync(AddDonationRequestDto donationRequest, Guid userId);
        Task<IActionResult> ExecutePaymentAsync(IQueryCollection query, Guid userId);
    }
}
