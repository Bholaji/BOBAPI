using System.ComponentModel.DataAnnotations;

namespace Bob.Model.DTO
{
	public class UserContactDTO
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
		public Guid? OrganizationId { get; set; }

	}
}
