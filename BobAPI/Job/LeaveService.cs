using Bob.Core;
using Bob.Core.Services.IServices;
using Bob.Migrations.Data;
using Bob.Model.Entities;
using Bob.Model.Entities.Home;
using Bob.Model.Enums;
using Humanizer;

namespace BobAPI.Job
{
	public class LeaveService: ILeaveService
	{
		private readonly IServiceScopeFactory scopeFactory;
        public ApplicationDbContext db;
		private readonly int _numberOfDaysForAutomaticLeaveApproval = 7;
        public LeaveService(IServiceScopeFactory scopeFactory)
        {
			this.scopeFactory = scopeFactory;
		}

        public async Task EndOfYearLeaveAccrual()
        {
            using var scope = scopeFactory.CreateScope();
			db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

			var usersWithoutLeave = db.Users.Where(u => !db.UserTimeOffs.Any(x => x.UserId == u.Id));

          //  var LeaveStatusPending = db.LeaveRequests.Where(u=> u.LeaveRequestStatus == LeaveRequestStatus.pending);

            foreach (var user in usersWithoutLeave)
            {
				DateTime userJoinDate = user.CreationDate;
				DateTime beginningOfYear = new (DateTime.Now.Year, 1, 1);
				DateTime endOfYear = new(userJoinDate.Year, 12, 31);

				int daysInYear = DateTime.IsLeapYear(userJoinDate.Year) ? 366 : 365;

				int holidaysPerYear = 20;

				double fractionOfYearRemaining = (daysInYear - userJoinDate.DayOfYear )/ daysInYear;

				double calculatedHolidays = Math.Round(holidaysPerYear * fractionOfYearRemaining, 1, MidpointRounding.AwayFromZero);

                var userId = user.Id;

				var newUserTimeOff = new UserTimeOff()
                {
                    UserId = userId,
                    Holdidays = 20,
                    Sickness_paid = 7,
                    WorkFromHome = "infinity",
                    Sickness_unpaid = "infinity",
                    Birthday=1,
                    MovingDay=2,
                    Compassionate=2
                };

				foreach (LeavePolicy leavePolicy in Enum.GetValues(typeof(LeavePolicy)))
				{
					var balanceActivity = new LeaveBalanceActivity()
					{
						UserId = userId,
						ActivityType = leavePolicy
					};

					LeaveDaysAccural? leaveDaysAccural = null;
					CarryOverActivity? carryOverActivity = null;

					switch (leavePolicy)
					{
						case LeavePolicy.Holiday:
							leaveDaysAccural = new LeaveDaysAccural()
							{
								UserId = userId,
								AccuralDate = userJoinDate,
								AccuralPeriod = $"{userJoinDate} - {endOfYear}",
								Amount = calculatedHolidays,
								Note = "Prorated allowance in days: 20 (20 base days allowance)",
								ActivityType= LeavePolicy.Holiday
							};
							double carryoverAmount = 0;

							if (userJoinDate.Year < beginningOfYear.Year)
							{
								carryoverAmount = (calculatedHolidays >= 5) ? 5 : (int)calculatedHolidays;
							}

							carryOverActivity = new CarryOverActivity()
							{
								UserId = userId,
								EffectiveDate = beginningOfYear,
								Amount = carryoverAmount,
								Description = "Carry over holiday from the previous year",
								UpdatedOn = beginningOfYear,
								ActivityType = LeavePolicy.Holiday
							};
							break;

						case LeavePolicy.Sickness_paid:
							leaveDaysAccural = new LeaveDaysAccural()
							{
								UserId = userId,
								AccuralDate = userJoinDate,
								AccuralPeriod = $"{userJoinDate} - {endOfYear}",
								Amount = 7,
								Note = "Prorated allowance in days: 7",
								ActivityType = LeavePolicy.Sickness_paid
							};

							carryOverActivity = new CarryOverActivity() { 
								UserId = userId,
								EffectiveDate = beginningOfYear,
								Amount = 0,
								Description = "No carryover",
								UpdatedOn = beginningOfYear,
								ActivityType = LeavePolicy.Sickness_paid
							};
							break;

						case LeavePolicy.Birthday:
							leaveDaysAccural = new LeaveDaysAccural()
							{
								UserId = userId,
								AccuralDate = userJoinDate,
								AccuralPeriod = $"{userJoinDate} - {endOfYear}",
								Amount = 1,
								Note = "Prorated allowance in days: 1",
								ActivityType = LeavePolicy.Birthday
							};

							carryOverActivity = new CarryOverActivity()
							{
								UserId = userId,
								EffectiveDate = beginningOfYear,
								Amount = 0,
								Description = "No carryover",
								UpdatedOn = beginningOfYear,
								ActivityType = LeavePolicy.Birthday
							};
							break;

						case LeavePolicy.Sickness_unpaid:
							leaveDaysAccural = new LeaveDaysAccural()
							{
								UserId = userId,
								AccuralDate = userJoinDate,
								AccuralPeriod = $"{userJoinDate} - {endOfYear}",
								Amount = 0,
								Note = "It is infinity",
								ActivityType = LeavePolicy.Sickness_unpaid
							};

							carryOverActivity = new CarryOverActivity()
							{
								UserId = userId,
								EffectiveDate = beginningOfYear,
								Amount = 0,
								Description = "No carryover",
								UpdatedOn = beginningOfYear,
								ActivityType = LeavePolicy.Sickness_unpaid
							};
							break;

						case LeavePolicy.WorkFromHome:
							leaveDaysAccural = new LeaveDaysAccural()
							{
								UserId = userId,
								AccuralDate = userJoinDate,
								AccuralPeriod = $"{userJoinDate} - {endOfYear}",
								Amount = 0,
								Note = "It is infinity",
								ActivityType = LeavePolicy.WorkFromHome
							};

							carryOverActivity = new CarryOverActivity()
							{
								UserId = userId,
								EffectiveDate = beginningOfYear,
								Amount = 0,
								Description = "No carryover",
								UpdatedOn = beginningOfYear,
								ActivityType = LeavePolicy.WorkFromHome
							};
							break;

						case LeavePolicy.Moving:
							leaveDaysAccural = new LeaveDaysAccural()
							{
								UserId = userId,
								AccuralDate = userJoinDate,
								AccuralPeriod = $"{userJoinDate} - {endOfYear}",
								Amount = 2,
								Note = "Prorated allowance in days: 2",
								ActivityType = LeavePolicy.Moving
							};

							carryOverActivity = new CarryOverActivity()
							{
								UserId = userId,
								EffectiveDate = beginningOfYear,
								Amount = 0,
								Description = "No carryover",
								UpdatedOn = beginningOfYear,
								ActivityType = LeavePolicy.Moving
							};
							break;

						case LeavePolicy.Compassionate:
							leaveDaysAccural = new LeaveDaysAccural()
							{
								UserId = userId,
								AccuralDate = userJoinDate,
								AccuralPeriod = $"{userJoinDate} - {endOfYear}",
								Amount = 2,
								Note = "Prorated allowance in days: 2",
								ActivityType = LeavePolicy.Compassionate
							};

							carryOverActivity = new CarryOverActivity()
							{
								UserId = userId,
								EffectiveDate = beginningOfYear,
								Amount = 0,
								Description = "No carryover",
								UpdatedOn = beginningOfYear,
								ActivityType = LeavePolicy.Compassionate
							};
							break;
					}

					await db.LeaveBalanceActivities.AddAsync(balanceActivity);
					await db.LeaveDaysAccurals.AddAsync(leaveDaysAccural);
					await db.CarryOverActivities.AddAsync(carryOverActivity);
				}
                await db.UserTimeOffs.AddAsync(newUserTimeOff);
            }

			await db.SaveChangesAsync();
        }

		public async Task CreateUserTimeOff()
		{
            using var scope = scopeFactory.CreateScope();
            db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var usersWithoutLeave = db.Users.Where(u => !db.UserTimeOffs.Any(x => x.UserId == u.Id));

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

		public async Task SystemApproveLeave()
		{
            using var scope = scopeFactory.CreateScope();
            db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
			var pendingLeaveRequests =  db.LeaveRequests.Where(x => x.LeaveRequestStatus == LeaveRequestStatus.pending);
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
    }
}
