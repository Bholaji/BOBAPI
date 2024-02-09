using System.ComponentModel.DataAnnotations;

namespace Bob.Model.DTO
{
	public class UserFinancialDTO
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

	}
}
