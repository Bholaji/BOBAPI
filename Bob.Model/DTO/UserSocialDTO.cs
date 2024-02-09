using System.ComponentModel.DataAnnotations;

namespace Bob.Model.DTO
{
	public class UserSocialDTO
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
	}
}
