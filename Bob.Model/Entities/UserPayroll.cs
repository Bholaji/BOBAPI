using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bob.Model.Entities
{
	public class UserPayroll : BaseEntity
	{
		public DateTime? EffectiveDate { get; set; }
		public int? BaseSalary { get; set; }
		public string? SalaryPayPeriod { get; set; }
		public string? SalaryPayFrequency { get; set; }
		public Guid? UserId { get; set; }
		[ForeignKey("UserId")]
		[ValidateNever]
		public User User { get; set; }
	}
}
