


namespace CharityHub.Business.ViewModels
{
    public class CampaignDto
    {
        public Guid CampaignId { get; set; }
        public int CampaignCode { get; set; }
        public string CampaignTitle { get; set; }
        public string CampaignThumbnail { get; set; }
        public string CampaignDescription { get; set; }
        public decimal TargetAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string PartnerName { get; set; }
        public string PartnerLogo { get; set; }
        public string CampaignStatus { get; set; }
        public string PartnerNumber { get; set; }
        public DateTime DateCreated { get; set; }

        public void PrintDates()
        {
            if (StartDate.HasValue)
            {
                string startDateFormatted = StartDate.Value.ToString("yyyy-MM-dd");
                Console.WriteLine("Start Date: " + startDateFormatted);
            }
            else
            {
                Console.WriteLine("Start Date is not set.");
            }

            if (EndDate.HasValue)
            {
                string endDateFormatted = EndDate.Value.ToString("yyyy-MM-dd");
                Console.WriteLine("End Date: " + endDateFormatted);
            }
            else
            {
                Console.WriteLine("End Date is not set.");
            }
        }
    }
}
