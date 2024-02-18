using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bob.Model.DTO.UserDTO
{
	public class UpdateUserRequest
	{
		[JsonIgnore]
		public Guid UserId { get; set; }

		[MaxLength(50)]
		public string? FirstName { get; set; }
		[MaxLength(50)]
		public string? Surname { get; set; }
		[MaxLength(100)]
		public string? FullName { get => $"{FirstName} {Surname}"; }
		[MaxLength(100)]
		public string? DispalyName { get; set; }
		public string? MiddleName { get; set; }
		public string? Email { get; set; }
		[MaxLength(50)]
		public string? Prefix { get; set; }
		[MaxLength(50)]
		public string? Pronouns { get; set; }
		public DateOnly? DateOfBirth { get; set; }
		[MaxLength(50)]
		public string? Nationality1 { get; set; }
		[MaxLength(50)]
		public string? Nationality2 { get; set; }
		[MaxLength(50)]
		public string? Language1 { get; set; }
		[MaxLength(50)]
		public string? Language2 { get; set; }
		public Guid? RoleId { get; set; }
		public Guid? OrganizationId { get; set; }
	}
}
