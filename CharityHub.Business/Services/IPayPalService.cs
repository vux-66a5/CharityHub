using CharityHub.Data.Models;
using Microsoft.AspNetCore.Http;

namespace CharityHub.Business.Services
{
    public interface IPayPalService
    {
        Task<string> CreatePaymentUrl(PaymentInformation model);
        //PaymentResponse PaymentExecute(IQueryCollection collections);
        Donation PaymentExecute(IQueryCollection collections);
        //Task<PaymentResponseModel> PaymentExecute(IQueryCollection collections);
    }
}
