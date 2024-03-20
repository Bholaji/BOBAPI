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

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			var now = DateTime.Now;
			var endOfYear = new DateTime(now.Year, 12, 31, 23, 59, 59);

			var delay = (int)(endOfYear - now).TotalMilliseconds;

			_timer = new Timer(ExecuteEndOfYearAccural, null, delay, Timeout.Infinite);

			await Task.CompletedTask;
		}

		private async void ExecuteEndOfYearAccural(object sender)
		{
			_logger.LogInformation("End of year leave accrual starting...");

			await _LeaveService.EndOfYearLeaveAccrual();

			var now = DateTime.Now;
			var nextYear = now.AddYears(1);
			var timeUntilNextYear = new DateTime(nextYear.Year, 1, 1, 0, 0, 0) - now;

			_timer.Change((int)timeUntilNextYear.TotalMilliseconds, Timeout.Infinite);

			_logger.LogInformation("Next End of Year Leave Accrual scheduled for {time}...", nextYear);
		}
	}
}
