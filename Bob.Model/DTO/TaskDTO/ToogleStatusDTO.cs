using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TaskStatus = Bob.Model.Enums.TaskStatus;

namespace Bob.Model.DTO.TaskDTO
{
	public class ToogleStatusDTO
	{
		[JsonIgnore]
		public Guid TaskId { get; set; }
		public TaskStatus TaskStatus { get; set; }
	}
}
