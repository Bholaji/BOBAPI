
namespace BobAPI.Job
{
	public class EndOfYearBackgroundWorkerService: BackgroundService
	{
		private readonly ILogger<EndOfYearBackgroundWorkerService> _logger;
		private Timer _timer;
		private int executionCount = 0;
		private readonly ILeaveService _LeaveService;
		public EndOfYearBackgroundWorkerService(ILogger<EndOfYearBackgroundWorkerService> logger, ILeaveService LeaveService)
		{
			_logger = logger;
			_LeaveService = LeaveService;
		}

		/*private async Task ExecuteEndOfYearAccrual()
		{
			_logger.LogInformation("End of year leave accrual starting...");
			await _LeaveService.EndOfYearLeaveAccrual();

			// Calculate the time until the end of the current year
			var now = DateTime.Now;
			var endOfYear = new DateTime(now.Year, 12, 31, 23, 59, 59);
			var timeUntilEndOfYear = endOfYear - now;

			// Add a fixed interval to schedule the next execution (e.g., one day)
			var interval = TimeSpan.FromDays(1); // Adjust the interval as needed
			var nextExecutionTime = timeUntilEndOfYear + interval;
		}

		*/
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			// Set up timer to trigger at the end of the current year
			var now = DateTime.Now;
			var endOfYear = new DateTime(now.Year, 12, 31, 23, 59, 59);
			var timeUntilEndOfYear = endOfYear - now;

			// Trigger the execution at the end of the current year
			//_timer = new Timer(async _ => await ExecuteEndOfYearAccrual(), null, timeUntilEndOfYear, Timeout.InfiniteTimeSpan);
		}

	}
}
