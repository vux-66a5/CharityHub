using CharityHub.Business.VNPay.Models;
using CharityHub.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharityHub.Business.VNPay.Services
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        Donation PaymentExecute(IQueryCollection collections);
    }
}
