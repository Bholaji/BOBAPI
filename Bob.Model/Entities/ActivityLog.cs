using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bob.Model.Entities
{
	public class ActivityLog
	{
		public Guid ActivityLogId { get; set; }
		public Guid TaskId { get; set; }
		public Guid UserId { get; set; }
		[ForeignKey("UserId")]
		[ValidateNever]
		public User User { get; set; }
		public string Activity { get; set; }
		public DateTime Timestamp { get; set; }
	}
}
