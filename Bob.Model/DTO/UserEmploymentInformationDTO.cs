using System.ComponentModel.DataAnnotations;

namespace Bob.Model.DTO
{
	public class UserEmploymentInformationDTO
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
	}
}
