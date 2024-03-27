using Bob.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Model.Entities
{
	public class LeaveRequest : BaseLeaveEntity
	{
		public LeavePolicy LeavePolicy { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public DateTime CreationDate { get; set; }
		public LeaveRequestDuration Duration { get; set; }
		public LeaveRequestDuration? Duration2 { get; set; }
		public Guid RequesterId { get; set; }
		[MaxLength(500)]
		public string? Description { get; set; }
		public double DaysRequested { get; set; }
		public string? ApprovedBy{ get; set; }
		public LeaveRequestStatus LeaveRequestStatus { get; set; }
	}
}
