using Bob.Model.DTO.LeaveDTO;
using Bob.Model.Entities;
using Bob.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Core.Strategy
{
	public class EditSameDayStrategy : IEditLeaveRequestStrategy
	{
		public async Task HandleEdit(EditRequestLeaveDTO DTO, LeaveRequest leaveRequest)
		{
			// Handle same-day scenario
			leaveRequest.Duration2 = null;
			leaveRequest.Duration = DTO.Duration1 ?? leaveRequest.Duration;
			leaveRequest.StartDate = DTO.StartDate ?? leaveRequest.StartDate.Date;

			if (DTO.Duration1 == LeaveRequestDuration.Half_Day)
			{
				// Handle half-day
				leaveRequest.EndDate = leaveRequest.StartDate.AddHours(4.5);
				leaveRequest.DaysRequested = 0.5;
			}
			else if (DTO.Duration1 == LeaveRequestDuration.All_Day)
			{
				// Handle full-day
				leaveRequest.EndDate = DTO.EndDate ?? leaveRequest.EndDate.Date;
				leaveRequest.DaysRequested = 1;
			}
		}
	}

	public class EditAllDayStrategy : IEditLeaveRequestStrategy
	{
		public async Task HandleEdit(EditRequestLeaveDTO DTO, LeaveRequest leaveRequest)
		{
			// Handle all-day scenario
			leaveRequest.Duration = DTO.Duration1 ?? leaveRequest.Duration;
			leaveRequest.Duration2 = DTO.Duration2 ?? leaveRequest.Duration2;
			leaveRequest.StartDate = DTO.StartDate ?? leaveRequest.StartDate.Date;

			if (DTO.Duration2 == LeaveRequestDuration.All_Day)
			{
				// Handle full-day end date
				leaveRequest.EndDate = DTO.EndDate ?? leaveRequest.EndDate.Date;
				leaveRequest.DaysRequested = (leaveRequest.EndDate - leaveRequest.StartDate).TotalDays + 1;
			}
			else if (DTO.Duration2 == LeaveRequestDuration.Half_Day)
			{
				// Handle half-day end date
				leaveRequest.EndDate = DTO.EndDate ?? leaveRequest.EndDate.Date.AddHours(4.5);
				leaveRequest.DaysRequested = (leaveRequest.EndDate - leaveRequest.StartDate).TotalDays + 0.5;
			}

		}
	}

	public class EditHalfDayStrategy : IEditLeaveRequestStrategy
	{
		public async Task HandleEdit(EditRequestLeaveDTO DTO, LeaveRequest leaveRequest)
		{
			// Handle half-day scenario
			leaveRequest.Duration = DTO.Duration1 ?? leaveRequest.Duration;
			leaveRequest.Duration2 = DTO.Duration2 ?? leaveRequest.Duration2;
			leaveRequest.StartDate = DTO.StartDate ?? leaveRequest.StartDate.Date.AddHours(12);

			if (DTO.Duration2 == LeaveRequestDuration.All_Day)
			{
				// Handle full-day end date
				leaveRequest.EndDate = DTO.EndDate ?? leaveRequest.EndDate.Date;
				leaveRequest.DaysRequested = (leaveRequest.EndDate - leaveRequest.StartDate).TotalDays + 0.5;
			}
			else if (DTO.Duration2 == LeaveRequestDuration.Half_Day)
			{
				// Handle half-day end date
				leaveRequest.EndDate = DTO.EndDate ?? leaveRequest.EndDate.Date.AddHours(4.5);
				leaveRequest.DaysRequested = (leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
			}
		}
	}

}
