using System.ComponentModel.DataAnnotations;

namespace Bob.Model.DTO
{
	public class UserPayrollDTO
    {
		public DateTime? EffectiveDate { get; set; }
        public int? BaseSalary { get; set; }
		[MaxLength(50)]
		public string? SalaryPayPeriod { get; set; }
		[MaxLength(50)]
		public string? SalaryPayFrequency { get; set; }
		public Guid? OrganizationId { get; set; }
	}
}
