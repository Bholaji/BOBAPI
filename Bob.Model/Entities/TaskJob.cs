using System.ComponentModel.DataAnnotations;

namespace Bob.Model.Entities
{
	public class TaskJob: BaseEntity
	{
		[MaxLength(50)]
		public string TaskName { get; set; }

		[MaxLength(200)]
		public string TaskDescription { get; set; }
		[MaxLength(50)]
		public string TaskList { get; set; }
		public DateTime DueDate { get; set; }
		public DateTime StartDate { get; set; }
		public List<UserTask> UserTasks { get; set; }
	}
}
