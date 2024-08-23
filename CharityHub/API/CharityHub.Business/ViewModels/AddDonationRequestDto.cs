using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharityHub.Business.ViewModels
{
    public class AddDonationRequestDto
    {
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public Guid CampaignId { get; set; }
    }
}
