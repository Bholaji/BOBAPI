using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Bob.Model.DTO
{
	public class UserFinancialDTO
	{
		[JsonIgnore]
		public Guid FinancialId { get; set; }
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
		public Guid? OrganizationId { get; set; }
	}
}
