using Bob.Core.Services.IServices;

namespace BobAPI.Job
{
	public class BackgroundWorkerService : BackgroundService
	{
		private readonly ILogger<BackgroundWorkerService> _logger;
		private Timer _timer;
		private int executionCount = 0;
		private readonly ILeaveService _LeaveService;
		public BackgroundWorkerService(ILogger<BackgroundWorkerService> logger, ILeaveService LeaveService)
		{
			_logger = logger;
			_LeaveService = LeaveService;
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_logger.LogInformation($"Service started at {DateTime.Now}........");

			var nextExecutionTime = 10 * 1000;

			_timer = new Timer(RunLeaveCreationTask, null, nextExecutionTime, Timeout.Infinite);
			return Task.CompletedTask;
		}

		private async void RunLeaveCreationTask(object sender)
		{
			_logger.LogInformation("Leave  creation about to start");

			await _LeaveService.CreateUserTimeOff();	
			await _LeaveService.SystemApproveLeave();

			var count = Interlocked.Increment(ref executionCount);
			var nextExecutionTime = 10 * 1000;
			_timer.Change(nextExecutionTime, Timeout.Infinite);

			_logger.LogInformation("Next Leave Creation reminder ran at {time}...", DateTime.Now);
		}
	}
}
