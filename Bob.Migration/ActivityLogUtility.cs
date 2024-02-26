using Bob.Model.Entities;
using Bob.Migrations.Data;

namespace Bob.Migrations
{
	internal class ActivityLogUtility
	{
		private readonly ApplicationDbContext _db;
		public ActivityLogUtility(ApplicationDbContext db)
        {
			_db = db;
        }

		public async Task LogActivity(UserTask task, bool isTaskCreation = false)
		{
			var currentUser = _db.Users.FirstOrDefault(u => u.Id == task.UserId);

			if (currentUser is null)
			{
				//throw new NotFoundException($"{nameof(User)} {ResponseMessage.NotFound}");
				throw new ArgumentNullException(nameof(task.User), "User cannot be null.");
			}

			var activityLog = new ActivityLog()
			{
				TaskId = task.TaskId,
				UserId = currentUser.Id,
				Activity = isTaskCreation ? $"Task created by {currentUser.DispalyName} at {DateTime.Now}"
							: $"Task status changed to {task.TaskStatus} by {currentUser.DispalyName} at {DateTime.Now}"
			};
			if (!isTaskCreation)
			{
				activityLog.Activity += $" Task was updated by {currentUser.DispalyName} at {DateTime.Now}";
			}



			await _db.ActivityLogs.AddAsync(activityLog);
		}

	}
}
