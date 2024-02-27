using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TaskStatus = Bob.Model.Enums.TaskStatus;

namespace Bob.Model.DTO.TaskDTO
{
	public class UpdateTaskDTO
	{
		[JsonIgnore]
		public Guid TaskId { get; set; }
		public Guid RequestedBy { get; set; }
		public List<Guid> RequestedFor { get; set; }
		public string? TaskName { get; set; }
		public string? TaskDescription { get; set; }
		public string? TaskList { get; set; }
		public DateTime? DueDate { get; set; }
		public DateTime? StartDate { get; set; }
		public TaskStatus? TaskStatus { get; set; }
		public bool? isGeneral { get; set; }
		public Guid OrganizationId { get; set; }
	}
}
