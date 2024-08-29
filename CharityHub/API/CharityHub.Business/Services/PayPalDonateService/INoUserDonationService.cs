using CharityHub.Business.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CharityHub.Business.Services.PayPalDonate
{
    public interface INoUserDonationService
    {
        Task<string> CreatePayPalDonationAsync(AddDonationRequestDto donationRequest);
        Task<string> ExecutePaymentAsync(IQueryCollection query);
    }
}