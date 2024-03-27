using Bob.Model.Enums;
using System.Text.Json.Serialization;

namespace Bob.Model.DTO.LeaveDTO
{
	public class EditRequestLeaveDTO
	{
		[JsonIgnore]
		public Guid LeaveRequestId { get; set; }
		public Guid RequesterId { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public LeaveRequestDuration? Duration1 { get; set; }
		public LeaveRequestDuration? Duration2 { get; set; }
	}
}
