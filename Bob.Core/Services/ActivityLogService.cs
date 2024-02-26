using Bob.Core.Exceptions;
using Bob.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTask = Bob.Model.Entities.UserTask;

namespace Bob.Core.Services
{
	public class ActivityLogService
	{
		public void LogActivity(UserTask task)
		{
			var currentUser = task.User;

			if (currentUser is null)
			{
				throw new NotFoundException($"{nameof(User)} {ResponseMessage.NotFound}");
			}
			 
			var activityLog = new ActivityLog()
			{
				TaskId = task.TaskId,
				UserId = currentUser.Id,
				Activity = $"Task status changed to {task.TaskStatus} by {currentUser.DispalyName} at {DateTime.Now}"
			};
		}
	}
}
