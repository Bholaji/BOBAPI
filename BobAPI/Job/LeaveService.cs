using Bob.Migrations.Data;
using Bob.Model.Entities;
using Bob.Model.Enums;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace BobAPI.Job
{
	public class LeaveService: ILeaveService
	{
		private readonly IServiceScopeFactory scopeFactory;
        public ApplicationDbContext db;
		private readonly int _numberOfDaysForAutomaticLeaveApproval = 7;
        private readonly ILogger<LeaveService> _logger;
        public LeaveService(IServiceScopeFactory scopeFactory, ILogger<LeaveService> logger)
        {
			this.scopeFactory = scopeFactory;
            _logger = logger;
		}

        public async Task EndOfYearLeaveAccrual()
        {

            try
            {
				using var scope = scopeFactory.CreateScope();
				db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

				var accrualStartperiod = DateTime.Now.Date;
				var endOfTheYear = new DateTime(accrualStartperiod.Year, 12, 31);

				var userTimeOffs = db.UserTimeOffs.Select(x => x).ToList();
				List<LeaveDaysAccural> leaveDaysAccural = [];
				List<CarryOverActivity> carryOverActivities = [];

				foreach (var timeOff in userTimeOffs)
				{

					var carryOverHoliday = timeOff.Holdidays > 5 ? 5 : timeOff.Holdidays;
					var currentHolidays = 20 + carryOverHoliday;
					timeOff.Holdidays = currentHolidays;
					timeOff.Sickness_paid = 7;
					timeOff.WorkFromHome = "infinity";
					timeOff.Sickness_unpaid = "infinity";
					timeOff.Birthday = 1;
					timeOff.MovingDay = 2;
					timeOff.Compassionate = 2;

					leaveDaysAccural.Add(new LeaveDaysAccural()
					{
						UserId = timeOff.UserId,
						AccuralDate = accrualStartperiod,
						AccuralPeriod = $"{accrualStartperiod} - {endOfTheYear}",
						Amount = currentHolidays,
						Note = "Prorated allowance in days: 20 (20 base days allowance)",
						ActivityType = LeavePolicy.Holiday
					});

					carryOverActivities.Add(new CarryOverActivity()
					{
						UserId = timeOff.UserId,
						EffectiveDate = accrualStartperiod,
						Amount = carryOverHoliday,
						Description = "Carry over holiday from the previous year",
						UpdatedOn = accrualStartperiod,
						ActivityType = LeavePolicy.Holiday
					});

					leaveDaysAccural.Add(new LeaveDaysAccural()
					{
						UserId = timeOff.UserId,
						AccuralDate = accrualStartperiod,
						AccuralPeriod = $"{accrualStartperiod} - {endOfTheYear}",
						Amount = 7,
						Note = "Prorated allowance in days: 7",
						ActivityType = LeavePolicy.Sickness_paid
					});

					leaveDaysAccural.Add(new LeaveDaysAccural()
					{
						UserId = timeOff.UserId,
						AccuralDate = accrualStartperiod,
						AccuralPeriod = $"{accrualStartperiod} - {endOfTheYear}",
						Amount = 1,
						Note = "Prorated allowance in days: 1",
						ActivityType = LeavePolicy.Birthday
					});

					leaveDaysAccural.Add(new LeaveDaysAccural()
					{
						UserId = timeOff.UserId,
						AccuralDate = accrualStartperiod,
						AccuralPeriod = $"{accrualStartperiod} - {endOfTheYear}",
						Amount = 0,
						Note = "It is infinity",
						ActivityType = LeavePolicy.Sickness_unpaid
					});

					leaveDaysAccural.Add(new LeaveDaysAccural()
					{
						UserId = timeOff.UserId,
						AccuralDate = accrualStartperiod,
						AccuralPeriod = $"{accrualStartperiod} - {endOfTheYear}",
						Amount = 0,
						Note = "It is infinity",
						ActivityType = LeavePolicy.WorkFromHome
					});


					leaveDaysAccural.Add(new LeaveDaysAccural()
					{
						UserId = timeOff.UserId,
						AccuralDate = accrualStartperiod,
						AccuralPeriod = $"{accrualStartperiod} - {endOfTheYear}",
						Amount = 2,
						Note = "Prorated allowance in days: 2",
						ActivityType = LeavePolicy.Compassionate
					});

					leaveDaysAccural.Add(new LeaveDaysAccural()
					{
						UserId = timeOff.UserId,
						AccuralDate = accrualStartperiod,
						AccuralPeriod = $"{accrualStartperiod} - {endOfTheYear}",
						Amount = 2,
						Note = "Prorated allowance in days: 2",
						ActivityType = LeavePolicy.Moving
					});
				}
				db.UserTimeOffs.UpdateRange(userTimeOffs);
				await db.LeaveDaysAccurals.AddRangeAsync(leaveDaysAccural);
				await db.CarryOverActivities.AddRangeAsync(carryOverActivities);
				await db.SaveChangesAsync();
			}
            catch (Exception ex)
            {
				_logger.LogInformation(ex.Message);
            }
        }

		public async Task CreateUserTimeOff()
		{
			try
			{
				using var scope = scopeFactory.CreateScope();
				db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

				var usersWithoutLeave = await db.Users.Where(u => !db.UserTimeOffs.Any(x => x.UserId == u.Id)).ToListAsync();

				foreach (var user in usersWithoutLeave)
				{
					DateTime userJoinDate = user.CreationDate;
					DateTime beginningOfYear = new(DateTime.Now.Year, 1, 1);
					DateTime endOfYear = new(userJoinDate.Year, 12, 31);

					int daysInYear = DateTime.IsLeapYear(userJoinDate.Year) ? 366 : 365;

					int holidaysPerYear = 20;

					double fractionOfYearRemaining = (daysInYear - userJoinDate.DayOfYear) / daysInYear;

					double calculatedHolidays = Math.Round(holidaysPerYear * fractionOfYearRemaining, 1, MidpointRounding.AwayFromZero);

					var userId = user.Id;

					var newUserTimeOff = new UserTimeOff()
					{
						UserId = userId,
						Holdidays = calculatedHolidays,
						Sickness_paid = 7,
						WorkFromHome = "infinity",
						Sickness_unpaid = "infinity",
						Birthday = 1,
						MovingDay = 2,
						Compassionate = 2
					};

					await db.UserTimeOffs.AddAsync(newUserTimeOff);
					await db.SaveChangesAsync();
				}
			}
			catch (Exception ex)
			{
				_logger.LogInformation(ex.Message);
			}
		}

		public async Task SystemApproveLeave()
		{
			try
			{
				using var scope = scopeFactory.CreateScope();
				db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
				var pendingLeaveRequests = db.LeaveRequests.Where(x => x.LeaveRequestStatus == LeaveRequestStatus.pending);
				var activityLogs = new List<ActivityLog>();
				foreach (var leaveRequests in pendingLeaveRequests)
				{
					if ((DateTime.Now - leaveRequests.CreationDate).Days > _numberOfDaysForAutomaticLeaveApproval)
					{
						leaveRequests.LeaveRequestStatus = LeaveRequestStatus.Approved;
					}
					activityLogs.Add(new ActivityLog
					{

					});
				}
				await db.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				_logger.LogInformation(ex.Message);
			}
			
        }
    }
}
