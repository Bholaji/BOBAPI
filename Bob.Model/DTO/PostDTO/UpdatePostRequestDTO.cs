using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bob.Model.DTO.PostDTO
{
	public class UpdatePostRequestDTO
	{
		[JsonIgnore]
		public Guid PostId { get; set; }
		[MaxLength(50)]
		public string? Title { get; set; }
		[MaxLength(500)]
		public string? Content { get; set; }
		public string? ImageUrl { get; set; }
	}
}
