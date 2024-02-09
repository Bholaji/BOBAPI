using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bob.Model.Entities
{
	public class UserContact : BaseEntity
	{
		[MaxLength(50)]
		public string? PersonalEmail { get; set; }
		[MaxLength(50)]
		public string? PhoneNumber { get; set; }
		[MaxLength(50)]
		public string? MobileNumber { get; set; }
		public int? PassportNumber { get; set; }
		public int? NationalId { get; set; }
		public int? SSN { get; set; }
		public int? TaxIdNumber { get; set; }
		public Guid UserId { get; set; }
		[ForeignKey("UserId")]
		[ValidateNever]
		public User User { get; set; }
	}
}
