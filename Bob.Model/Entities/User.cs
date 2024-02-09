using System.ComponentModel.DataAnnotations;

namespace Bob.Model.Entities
{
	public class User : BaseEntity
	{
		[Required]
		[MaxLength(50)]
		public string FirstName { get; set; }
		[Required]
		[MaxLength(50)]
		public string Surname { get; set; }
		[MaxLength(100)]
		public string FullName { get; set; }
		public string? DispalyName { get; set; }
		public string? MiddleName { get; set; }
		[Required]
		public string Email { get; set; }
		public string? Prefix { get; set; }
		public string? Pronouns { get; set; }
		[Required]
		public DateOnly DateOfBirth { get; set; }
		[Required]
		public string Nationality1 { get; set; }
		public string? Nationality2 { get; set; }
		[Required]
		public string Language1 { get; set; }
		public string? Language2 { get; set; }
		public UserContact userContact { get; set; }
		public UserAddress UserAddress { get; set; }
		public UserSocial UserSocial { get; set; }
		public UserFinancial UserFinancial { get; set; }
		public UserPayroll UserPayroll { get; set; }
		public UserEmploymentInformation UserEmploymentInformation { get; set; }
		public string SetFullName() => FullName = $"{FirstName} {Surname}";
	}
}
