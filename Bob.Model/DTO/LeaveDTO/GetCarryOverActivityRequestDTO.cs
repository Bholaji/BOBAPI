using Bob.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bob.Model.DTO.LeaveDTO
{
	public class GetCarryOverActivityRequestDTO
	{
		[JsonIgnore]
		public Guid UserId { get; set; }
		public LeavePolicy LeavePolicy { get; set; }
	}
}
