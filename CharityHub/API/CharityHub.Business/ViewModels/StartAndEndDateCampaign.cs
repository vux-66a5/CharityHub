namespace CharityHub.Business.ViewModels
{
    public class StartAndEndDateCampaign
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

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