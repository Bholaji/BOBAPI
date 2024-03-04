using System.Text.Json.Serialization;
using TaskStatus = Bob.Model.Enums.TaskStatus;

namespace Bob.Model.DTO.TaskDTO
{
	public class ToogleStatusDTO
	{
		[JsonIgnore]
		public Guid TaskId { get; set; }
		public Guid UserId { get; set; }
		public TaskStatus TaskStatus { get; set; }
	}
}
