using Bob.Core.Services.IServices;

namespace BobAPI.Job
{
	public class BackgroundWorkerService : BackgroundService
	{
		private readonly ILogger<BackgroundWorkerService> _logger;
		private Timer _timer;
		private int executionCount = 0;
		private readonly IServiceScopeFactory _scopeFactory;
		private readonly ILeaveService _LeaveService;
		public BackgroundWorkerService(ILogger<BackgroundWorkerService> logger, IServiceScopeFactory scopeFactory, ILeaveService LeaveService)
        {
			_logger = logger;
			_LeaveService = LeaveService;
			_scopeFactory = scopeFactory;
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_logger.LogInformation($"Service started at {DateTime.Now}........");

			var nextExecutionTime = 10 * 1000;
			var nextSchedulrfTime = DateTime.Now.AddMilliseconds(nextExecutionTime);

			_timer = new Timer(CreateUserTimeOff, null, nextExecutionTime, Timeout.Infinite);
			return Task.CompletedTask;
		}

		private async void CreateUserTimeOff(object sender)
		{
			_logger.LogInformation("Leave  creation about to start");

			await _LeaveService.EndOfYearLeaveAccrual();
			await _LeaveService.CreateUserTimeOff();
			await _LeaveService.SystemApproveLeave();

			var count = Interlocked.Increment(ref executionCount);
			var nextExecutionTime = 10 * 1000;
			var nextScheduledTime = DateTime.Now.AddMilliseconds(nextExecutionTime);
			_timer.Change(nextExecutionTime, Timeout.Infinite);

			_logger.LogInformation("Next Leave Creation reminder ran at {time}...", DateTime.Now);
		}

		/*public Task StartAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Service started.");

			var nextExecutionTime = 60 * 1000;
			var nextSchedulrfTime = DateTime.Now.AddMilliseconds(nextExecutionTime);

			_timer = new Timer(StartLeaveReminder, null, nextExecutionTime, Timeout.Infinite);
			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Service stopped.");
			return Task.CompletedTask;
		}*/
	}
}
