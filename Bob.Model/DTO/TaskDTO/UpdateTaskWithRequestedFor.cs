using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bob.Model.DTO.TaskDTO
{
	public class UpdateTaskWithRequestedFor
	{
		[JsonIgnore]
		public Guid TaskJobId { get; set; }
		public Guid RequestedBy { get; set; }
		public List<Guid> RequestedFor { get; set; }
		/*public string? TaskName { get; set; }
		public string? TaskDescription { get; set; }
		public string? TaskList { get; set; }
		public DateTime? DueDate { get; set; }
		public DateTime? StartDate { get; set; }*/
		public bool isGeneral { get; set; }
		public Guid OrganizationId { get; set; }
	}
}
