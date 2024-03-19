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
        public LeaveService(IServiceScopeFactory scopeFactory)
        {
			this.scopeFactory = scopeFactory;
		}

        public async Task LeaveServices()
        {
            using var scope = scopeFactory.CreateScope();
			db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

			var usersWithoutLeave = db.Users.Where(u => !db.UserTimeOffs.Any(x => x.UserId == u.Id));

            var LeaveStatusPending = db.LeaveRequests.Where(u=> ((int)u.LeaveRequestStatus) == 1);

            foreach (var user in usersWithoutLeave)
            {
				DateTime userJoinDate = user.CreationDate;
				DateTime beginningOfYear = new (DateTime.Now.Year, 1, 1);
				DateTime endOfYear = new(userJoinDate.Year, 12, 31);

				int daysInYear = DateTime.IsLeapYear(userJoinDate.Year) ? 366 : 365;

				int holidaysPerYear = 20;

				double fractionOfYearRemaining = (double)(daysInYear - userJoinDate.DayOfYear) / daysInYear;
				double calculatedHolidays = (double)(holidaysPerYear * fractionOfYearRemaining);

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


			foreach (var leaveStatus in LeaveStatusPending)
			{
				TimeSpan timeSpan = DateTime.UtcNow - leaveStatus.CreationDate;
				TimeSpan deleteThreshold = TimeSpan.FromDays(7);

				if (timeSpan.TotalDays > deleteThreshold.TotalDays)
				{
					leaveStatus.LeaveRequestStatus = LeaveRequestStatus.Approved;
					leaveStatus.ApprovedBy = "System";
				}

				db.LeaveRequests.Update(leaveStatus);
			}
			await db.SaveChangesAsync();
        }
    }
}
