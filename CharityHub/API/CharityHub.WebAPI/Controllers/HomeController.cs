using CharityHub.Business.Services;
using CharityHub.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace CharityHub.WebAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPayPalService _payPalService;

        public HomeController(IPayPalService payPalService)
        {
            _payPalService = payPalService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreatePaymentUrl(PaymentInformation model)
        {
            var url = await _payPalService.CreatePaymentUrl(model);

            return Redirect(url);
        }

        public IActionResult PaymentCallback()
        {
            var response = _payPalService.PaymentExecute(Request.Query);

            return Json(response);
        }
    }
}