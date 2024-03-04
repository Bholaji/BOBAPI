using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TaskStatus = Bob.Model.Enums.TaskStatus;

namespace Bob.Model.DTO.TaskDTO
{
	public class CreateTaskRequestDTO
	{
		[JsonIgnore]
		public Guid TaskId { get; set; }
		public string TaskName { get; set; }
		[Required]
		public Guid RequestedBy { get; set; }
		public string TaskDescription { get; set; }
		public string TaskList { get; set; }
		public DateTime DueDate { get; set; }
		public DateTime StartDate { get; set; }
		public TaskStatus TaskStatus { get; set; }
		public bool isGeneral { get; set; }
		public List<Guid> RequestedFor { get; set; }
		[Required]
		public Guid OrganizationId { get; set; }
	}
}
