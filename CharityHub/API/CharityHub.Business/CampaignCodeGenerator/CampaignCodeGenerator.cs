using CharityHub.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharityHub.Business.CampaignCodeGenerator
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
