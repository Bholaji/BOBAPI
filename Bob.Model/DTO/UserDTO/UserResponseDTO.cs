using System.ComponentModel.DataAnnotations;

namespace Bob.Model.DTO.UserDTO
{
	public class UserResponseDTO : BaseDto
	{
		[Required]
		[MaxLength(50)]
		public string FirstName { get; set; }
		[Required]
		[MaxLength(50)]
		public string Surname { get; set; }
		[MaxLength(100)]
		public string FullName { get => $"{FirstName} {Surname}"; }
		[MaxLength(50)]
		public string? DispalyName { get; set; }
		[MaxLength(50)]
		public string? MiddleName { get; set; }
		[Required]
		[MaxLength(50)]
		public string Email { get; set; }
		[MaxLength(50)]
		public string? Prefix { get; set; }
		[MaxLength(50)]
		public string? Pronouns { get; set; }
		[Required]
		public DateOnly DateOfBirth { get; set; }
		[Required]
		[MaxLength(50)]
		public string Nationality1 { get; set; }
		[MaxLength(50)]
		public string? Nationality2 { get; set; }
		[Required]
		[MaxLength(50)]
		public string Language1 { get; set; }
		[MaxLength(50)]
		public string? Language2 { get; set; }
		public Guid OrganizationId { get; set; }

	}
}
