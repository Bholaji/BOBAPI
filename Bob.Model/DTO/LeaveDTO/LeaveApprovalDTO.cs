using Bob.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bob.Model.DTO.LeaveDTO
{
	public class LeaveApprovalDTO
	{
		[JsonIgnore]
		public Guid ManagerId { get; set; }
		public Guid RequesterId { get; set; }
		public LeaveRequestStatus LeaveRequestStatus { get; set; }
	}
}
