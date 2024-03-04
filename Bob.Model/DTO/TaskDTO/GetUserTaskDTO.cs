using Bob.Model.DTO.PaginationDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
//using PaginationDTO = Bob.Model.DTO.PaginationDTO.PaginationDTO;

namespace Bob.Model.DTO.TaskDTO
{
	public class GetUserTaskDTO
	{
		[JsonIgnore]
		public Guid UserId { get; set; }
		public Guid TaskId { get; set; }
		public string TaskName { get; set; }
		public string RequestedFor { get; set; }
		public string TaskDescription { get; set; }
		public string TaskList { get; set; }
		public DateTime DueDate { get; set; }
		public DateTime StartDate { get; set; }
		public TaskStatus TaskStatus { get; set; }

	}
}
