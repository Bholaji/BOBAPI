using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bob.Model.Entities
{
	public class UserEmploymentInformation : BaseEntity
	{
		public DateTime? EffectiveDate { get; set; }
		public DateTime? EmploymentDate { get; set; }
		[MaxLength(50)]
		public string? Type { get; set; }
		[MaxLength(50)]
		public string? Contract { get; set; }
		[MaxLength(50)]
		public string? WeeklyHours { get; set; }
		[MaxLength(50)]
		public string? WorkingPattern { get; set; }

		[ForeignKey("UserId")]
		[ValidateNever]
		public User User { get; set; }
	}
}
