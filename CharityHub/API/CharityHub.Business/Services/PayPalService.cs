using CharityHub.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net;
using PayPal.Core;
using PayPal.v1.Payments;

namespace CharityHub.Business.Services
{
    public class PayPalService : IPayPalService
    {
        private readonly IConfiguration _configuration;
        private const double ExchangeRate = 22_863.0;

        public PayPalService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static double ConvertVndToDollar(double vnd)
        {
            var total = Math.Round(vnd / ExchangeRate, 2);

            return total;
        }

        public async Task<string> CreatePaymentUrl(PaymentInformation model)
        {
            // var envProd = new LiveEnvironment(_configuration["PaypalProduction:ClientId"],
            //     _configuration["PaypalProduction:SecretKey"]);

            var envSandbox =
                new SandboxEnvironment(_configuration["Paypal:ClientId"], _configuration["Paypal:SecretKey"]);
            var client = new PayPalHttpClient(envSandbox);
            var paypalOrderId = DateTime.Now.Ticks;
            var urlCallBack = _configuration["PaymentCallBack:ReturnUrl"];
            var payment = new PayPal.v1.Payments.Payment()
            {
                Intent = "sale",
                Transactions = new List<Transaction>()
                {
                    new Transaction()
                    {
                        Amount = new Amount()
                        {
                            Total = ConvertVndToDollar(model.Amount).ToString(),
                            Currency = "USD"
                        }
                    }
                },
                RedirectUrls = new RedirectUrls()
                {
                    ReturnUrl =
                        $"{urlCallBack}?payment_method=PayPal&success=1&order_id={paypalOrderId}&amount={model.Amount}",
                    CancelUrl =
                        $"{urlCallBack}?payment_method=PayPal&success=0&order_id={paypalOrderId}&amount={model.Amount}"
                },
                Payer = new Payer()
                {
                    PaymentMethod = "paypal"
                }
            };

            var request = new PaymentCreateRequest();
            request.RequestBody(payment);

            var paymentUrl = "";
            var response = await client.Execute(request);
            var statusCode = response.StatusCode;

            if (statusCode is not (HttpStatusCode.Accepted or HttpStatusCode.OK or HttpStatusCode.Created))
                return paymentUrl;

            var result = response.Result<Payment>();
            using var links = result.Links.GetEnumerator();

            while (links.MoveNext())
            {
                var lnk = links.Current;
                if (lnk == null) continue;
                if (!lnk.Rel.ToLower().Trim().Equals("approval_url")) continue;
                paymentUrl = lnk.Href;
            }

            return paymentUrl;

        }

        //public PaymentResponse PaymentExecute(IQueryCollection collections)
        //{
        //    var response = new PaymentResponse();

        //    foreach (var (key, value) in collections)
        //    {
        //        if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("order_description"))
        //        {
        //            response.OrderDescription = value;
        //        }

        //        if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("transaction_id"))
        //        {
        //            response.TransactionId = value;
        //        }

        //        if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("order_id"))
        //        {
        //            response.OrderId = value;
        //        }

        //        if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("payment_method"))
        //        {
        //            response.PaymentMethod = value;
        //        }

        //        if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("success"))
        //        {
        //            response.Success = Convert.ToInt32(value) > 0;
        //        }

        //        if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("paymentid"))
        //        {
        //            response.PaymentId = value;
        //        }

        //        if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("payerid"))
        //        {
        //            response.PayerId = value;
        //        }
        //        // Nếu Amount có trong query string
        //        if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("amount"))
        //        {
        //            response.Amount = Convert.ToDouble(value);
        //        }
        //    }

        //    return response;
        //}
        public Donation PaymentExecute(IQueryCollection collections)
        {
            var response = new Donation();

            response.DonationId = Guid.NewGuid();
            response.UserId = Guid.NewGuid();
            response.CampaignId = Guid.NewGuid();
            response.DateDonated = DateTime.Now;


            foreach (var (key, value) in collections)
            {

                if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("payment_method"))
                {
                    response.PaymentMethod = value;
                }

                if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("success"))
                {
                    response.IsConfirm = Convert.ToInt32(value) > 0;
                }
                if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("amount"))
                {
                    response.Amount = (decimal)Convert.ToDouble(value);
                }
            }
            return response;
        }
    }
}