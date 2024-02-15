using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bob.Model.DTO.CommentDTO
{
	public class GetCommentDTO
	{
		[MaxLength(500)]
		public string CommentBody { get; set; }
		[JsonIgnore]
		public Guid? CommentId { get; set; }

		[JsonIgnore]
		public Guid? PostId { get; set; }
	}
}
