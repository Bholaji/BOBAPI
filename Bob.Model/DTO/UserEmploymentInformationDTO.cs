﻿using Bob.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace Bob.Model.DTO
{
	public class UserEmploymentInformationDTO
	{
		public DateTime? EffectiveDate { get; set; }
		public DateTime? EmploymentDate { get; set; }
		[MaxLength(50)]
		public string? Type { get; set; }
		public string? JobTtle { get; set; }
		public EmploymentType? EmploymentType { get; set; }
		public EmploymentContract? EmploymentContract { get; set; }
		[MaxLength(50)]
		public string? WeeklyHours { get; set; }
		[MaxLength(50)]
		public string? WorkingPattern { get; set; }
		public Guid? OrganizationId { get; set; }
		public Guid? DepartmentId { get; set; }
	}
}
