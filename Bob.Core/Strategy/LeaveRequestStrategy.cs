using Bob.Model.DTO.LeaveDTO;
using Bob.Model.Entities;
using Bob.Model.Enums;

namespace Bob.Core.Strategy
{
	public class SameDayStrategy : ILeaveRequestStrategy
	{
		public async Task HandleRequest(LeaveRequestDTO DTO, LeaveRequest leaveRequest, double numberOfDaysRequested)
		{
			// Handle same-day scenario
			leaveRequest.Duration2 = null;
			leaveRequest.Duration = DTO.Duration1;
			leaveRequest.StartDate = leaveRequest.StartDate.Date;

			if (DTO.Duration1 == LeaveRequestDuration.Half_Day)
			{
				// Handle half-day
				leaveRequest.EndDate = leaveRequest.StartDate.AddHours(4.5);
				leaveRequest.DaysRequested = 0.5;
			}
			else if (DTO.Duration1 == LeaveRequestDuration.All_Day)
			{
				// Handle full-day
				leaveRequest.EndDate = leaveRequest.EndDate.Date;
				leaveRequest.DaysRequested = 1;
			}
		}
	}
	public class AllDayStrategy : ILeaveRequestStrategy
	{
		public async Task HandleRequest(LeaveRequestDTO DTO, LeaveRequest leaveRequest, double numberOfDaysRequested)
		{
			// Handle all-day scenario
			leaveRequest.Duration = LeaveRequestDuration.All_Day;
			leaveRequest.StartDate = leaveRequest.StartDate.Date;

			if (DTO.Duration2 == LeaveRequestDuration.All_Day)
			{
				// Handle full-day end date
				leaveRequest.DaysRequested = numberOfDaysRequested;
				leaveRequest.EndDate = leaveRequest.EndDate.Date;
			}
			else if (DTO.Duration2 == LeaveRequestDuration.Half_Day)
			{
				// Handle half-day end date
				leaveRequest.EndDate = leaveRequest.EndDate.Date.AddHours(4.5);
				leaveRequest.DaysRequested = numberOfDaysRequested - 0.5;
			}
		}
	}
	public class HalfDayStrategy : ILeaveRequestStrategy
	{
		public async Task HandleRequest(LeaveRequestDTO DTO, LeaveRequest leaveRequest, double numberOfDaysRequested)
		{
			// Handle half-day scenario
			leaveRequest.Duration = LeaveRequestDuration.Half_Day;
			leaveRequest.StartDate = leaveRequest.StartDate.Date.AddHours(4.5);

			if (DTO.Duration2 == LeaveRequestDuration.All_Day)
			{
				// Handle full-day end date
				leaveRequest.EndDate = leaveRequest.EndDate.Date;
				leaveRequest.DaysRequested = numberOfDaysRequested - 0.5;
			}
			else if (DTO.Duration2 == LeaveRequestDuration.Half_Day)
			{
				// Handle half-day end date
				leaveRequest.EndDate = leaveRequest.EndDate.Date.AddHours(4.5);
				leaveRequest.DaysRequested = numberOfDaysRequested - 1;
			}
		}
	}
}
