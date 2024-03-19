using Bob.Model.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Bob.Model.DTO.LeaveDTO
{
	public class LeaveRequestDTO
	{
		public LeavePolicy LeavePolicy { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public Guid RequesterId { get; set; }
		[MaxLength(500)]
		public string? Description { get; set; }
		public LeaveRequestDuration Duration1 { get; set; }
		public LeaveRequestDuration Duration2 { get; set; }
		//public int DaysRequested { get; set; }
		//public string Duration { get; set; }
		[JsonIgnore]
		public DateTime CreationDate { get; set; } = DateTime.UtcNow;
	}
}
