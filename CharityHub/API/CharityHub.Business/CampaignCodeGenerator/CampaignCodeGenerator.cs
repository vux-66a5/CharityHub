using CharityHub.Data.Data;

namespace CharityHub.Business
{
    public static class CampaignCodeGenerator
    {
        private static Random random = new Random();

        public static int GenerateUniqueCampaignCode(CharityHubDbContext dbContext)
        {
            int code;
            bool isUnique;

            do
            {
                code = random.Next(100000, 999999);
                isUnique = !dbContext.Campaigns.Any(c => c.CampaignCode == code);
            } while (!isUnique);

            return code;
        }
    }
}
