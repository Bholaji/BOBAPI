using System.Text.Json.Serialization;

namespace Bob.Model.DTO.TaskDTO
{
	public class UpdateTaskDTO
	{
		[JsonIgnore]
		public Guid TaskJobId { get; set; }
		public Guid RequestedBy { get; set; }
		public string? TaskName { get; set; }
		public string? TaskDescription { get; set; }
		public string? TaskList { get; set; }
		public DateTime? DueDate { get; set; }
		public DateTime? StartDate { get; set; }
		public Guid OrganizationId { get; set; }
	}
}
