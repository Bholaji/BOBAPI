
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

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_logger.LogInformation($"Service started at {DateTime.Now}........");

			var date = DateTime.Now;
			var nextYear = date.AddYears(1).Date;
			var timeUntilNextYear = nextYear - date;
			var x = timeUntilNextYear.Milliseconds;

			_timer = new Timer(RunLeaveCreationTask, null, x, Timeout.Infinite);
			return Task.CompletedTask;

		}

		private async void RunLeaveCreationTask(object sender)
		{
			_logger.LogInformation("Beginning of the year worker about to start");

			await _LeaveService.EndOfYearLeaveAccrual();

			var count = Interlocked.Increment(ref executionCount);
			var date = DateTime.Now;
			var nextYear = date.AddYears(1).Date;
			var timeUntilNextYear = nextYear - date;
			var x = timeUntilNextYear.Milliseconds;

			_timer.Change(x, Timeout.Infinite);

			_logger.LogInformation("Next worker reminder ran at {time}...", DateTime.Now);
		}

	}
}
