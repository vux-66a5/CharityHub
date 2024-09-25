//using CharityHub.Business.Services;
//using CharityHub.Business.Services.MomoService;
//using CharityHub.Business.ViewModels;
//using CharityHub.Data.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace CharityHub.WebAPI.Controllers
//{
//    public class HomeController : Controller
//    {
//        private readonly IPayPalService _payPalService;
//        private readonly IMomoService momoService;

//        public HomeController(IPayPalService payPalService, IMomoService momoService)
//        {
//            _payPalService = payPalService;
//            this.momoService = momoService;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> CreatePaymentUrl(MomoPaymentInfomation model)
//        {
//            var response = await momoService.CreatePaymentAsync(model);
//            return Redirect(response.PayUrl);
//        }

//        [HttpGet]
//        public IActionResult PaymentCallBack()
//        {
//            var response = momoService.PaymentExecuteAsync(HttpContext.Request.Query);
//            return View(response);
//        }
//    }
//}