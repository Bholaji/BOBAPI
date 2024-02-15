using Bob.Model.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bob.Model.Entities
{
	public class UserEmploymentInformation : BaseEntity
	{
		public int EmployeeID { get; set; }
		public string? JobTtle { get; set; }
        public Guid DepartmentId { get; set; }
		[ForeignKey(nameof(DepartmentId))]
		[ValidateNever]
		public Department Department { get; set; }
        public DateTime? EffectiveDate { get; set; }
		public DateTime? EmploymentDate { get; set; }
		[MaxLength(50)]
		public string? Type { get; set; }
		public EmploymentType? EmploymentType { get; set; }
		public EmploymentContract? EmploymentContract { get; set; }
		[MaxLength(50)]
		public string? WeeklyHours { get; set; }
		[MaxLength(50)]
		public string? WorkingPattern { get; set; }
		public Guid? UserId { get; set; }
		[ForeignKey("UserId")]
		[ValidateNever]
		public User User { get; set; }
	}
}
