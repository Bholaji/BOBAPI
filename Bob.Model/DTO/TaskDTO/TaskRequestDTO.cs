using System.Text.Json.Serialization;
using TaskStatus = Bob.Model.Enums.TaskStatus;

namespace Bob.Model.DTO.TaskDTO
{
	public class TaskRequestDTO
	{
		[JsonIgnore]
		public Guid TaskId { get; set; }
		public string TaskName { get; set; }
		public string RequestedFor { get; set; }
		public string TaskDescription { get; set; }
		public string TaskList { get; set; }
		public DateOnly DueDate { get; set; }
		public DateOnly StartDate { get; set; }
		public TaskStatus TaskStatus { get; set; }
		public Guid UserId { get; set; }
	}
}
