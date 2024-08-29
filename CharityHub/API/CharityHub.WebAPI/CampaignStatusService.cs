using CharityHub.Data.Data;
using CharityHub.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CharityHub.WebAPI
{
    public class CampaignStatusService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public CampaignStatusService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<CharityHubDbContext>();
                    var currentTime = DateTime.Now;

                    var campaignsToStart = await dbContext.Campaigns
                        .Where(c => c.CampaignStatus == "New" && c.StartDate <= currentTime)
                        .ToListAsync();

                    foreach (var campaign in campaignsToStart)
                    {
                        campaign.CampaignStatus = "InProgress";
                    }

                    var campaignsToClose = await dbContext.Campaigns
                        .Where(c => c.CampaignStatus == "InProgress" &&
                                    (c.EndDate <= currentTime || c.CurrentAmount >= c.TargetAmount))
                        .ToListAsync();

                    foreach (var campaign in campaignsToClose)
                    {
                        campaign.CampaignStatus = "Closed";
                    }

                    await dbContext.SaveChangesAsync();
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }

}
