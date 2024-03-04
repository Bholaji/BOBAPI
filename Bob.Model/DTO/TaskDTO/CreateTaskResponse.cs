using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bob.Model.DTO.TaskDTO
{
	public class CreateTaskResponse
	{
		[JsonIgnore]
		public Guid TaskId { get; set; }
	}
}
