using CharityHub.Business.Services;
using CharityHub.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace CharityHub.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IPayPalService _payPalService;

        public PaymentController(IPayPalService payPalService)
        {
            _payPalService = payPalService;
        }

        [HttpPost("Payment")]
        public async Task<IActionResult> CreatePaymentUrl(PaymentInformation model)
        {
            var url = await _payPalService.CreatePaymentUrl(model);

            return Redirect(url);
        }
        [HttpGet]
        [Route("Payment/Callback")]
        public IActionResult PaymentCallback()
        {
            var response = _payPalService.PaymentExecute(Request.Query);

            return Json(response);
        }
    }
}
