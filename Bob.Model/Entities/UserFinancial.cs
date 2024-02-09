using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bob.Model.Entities
{
	public class UserFinancial : BaseEntity
	{
		[MaxLength(50)]
		public string? AccountName { get; set; }
		public int? RatingNumber { get; set; }
		public int? AccountNumber { get; set; }
		[MaxLength(50)]
		public string? BankName { get; set; }
		[MaxLength(50)]
		public string? BankAccountType { get; set; }
		[MaxLength(50)]
		public string? BankAddress { get; set; }

		[ForeignKey("UserId")]
		[ValidateNever]
		public User User { get; set; }
	}
}
