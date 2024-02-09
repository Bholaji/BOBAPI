using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bob.Model.Entities
{
	public class UserSocial : BaseEntity
	{
		[MaxLength(50)]
		public string? About { get; set; }
		[MaxLength(50)]
		public string? Socials { get; set; }
		[MaxLength(50)]
		public string? Hobbies { get; set; }
		[MaxLength(50)]
		public string? Superpowers { get; set; }
		[MaxLength(50)]
		public string? FoodPrefrence { get; set; }

		[ForeignKey("UserId")]
		[ValidateNever]
		public User User { get; set; }
	}
}
