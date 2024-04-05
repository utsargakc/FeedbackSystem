using AuthWeb.Data;
using AuthWeb.Data.Migrations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace AuthWeb.Services
{
    public class RepeatingService : BackgroundService
    {
        private readonly IServiceProvider _provider;
        private readonly PeriodicTimer _timer = new(TimeSpan.FromSeconds(10));
        public RepeatingService(IServiceProvider provider)
        {
            _provider = provider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
            {
                CheckStatusAsync();
                using(var scope = _provider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    DateOnly dateToday = DateOnly.FromDateTime(DateTime.Now);
                    var topics = dbContext.topics.ToList();
                    foreach (var topic in topics)
                    {
                        if (topic.startDate != DateOnly.MinValue || topic.endDate != DateOnly.MinValue)
                        {
                            if (dateToday >= (topic.startDate) && dateToday <= (topic.endDate))
                            {
                                var data = topic;
                                data.Status = "Active";
                                dbContext.SaveChanges();
                            }
                            else
                            {
                                var data = topic;
                                data.Status = "Inactive";
                                dbContext.SaveChanges();
                            }
                        }
                    }

                }
            }
        }
        
        private static async Task CheckStatusAsync()
        {
            Console.WriteLine(DateTime.Now.ToString("O"));
            //await Task.Delay(1000);
        }
    }
}
