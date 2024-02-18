using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Bob.Model.DTO
{
	public class UserSocialDTO
	{
        [JsonIgnore]
        public Guid SocialId { get; set; }
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
		public Guid? OrganizationId { get; set; }
	}
}
