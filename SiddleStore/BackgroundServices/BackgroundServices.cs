using DataAcess.DAO;

namespace SiddleStore.BackgroundServices
{
    public class BackgroundServices : IHostedService, IDisposable
    {
        Timer? timer;
        ILogger logger;

        public BackgroundServices(ILogger<BackgroundServices> logger)
        {
            this.logger = logger;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            string start = "23:59:59";
            TimeSpan startTime = TimeSpan.Parse(start);
            TimeSpan period = startTime - DateTime.Now.TimeOfDay;

            logger.LogInformation("[BackgroundServices] at: " + DateTime.Now);
            timer = new Timer(Execute, null, period, TimeSpan.FromHours(24));
            await Task.Delay(5000);
            logger.LogInformation("[BackgroundServices] started at " + DateTime.Now);
            await Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask; ;
        }

        private void Execute(object? state)
        {
            logger.LogInformation("[BackgroundServices] excuted at " + DateTime.Now);
            DailyRevenueDAO.Instance.CreateDailyRevenues();
        } 
    }
}
