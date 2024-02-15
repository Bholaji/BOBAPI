using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bob.Model.Entities
{
	public class UserAddress : BaseEntity
	{
		[MaxLength(50)]
		public string? AddressLine1 { get; set; }
		[MaxLength(50)]
		public string? AddressLine2 { get; set; }
		[MaxLength(50)]
		public string? City { get; set; }
		public int? PostalCode { get; set; }
		[MaxLength(50)]
		public string? Country { get; set; }
		[MaxLength(50)]
		public string? State { get; set; }
		[MaxLength(50)]
		public string? ModifiedBy { get; set; }
		public Guid? UserId { get; set; }
		[ForeignKey("UserId")]
		[ValidateNever]
		public User User { get; set; }
	}
}
