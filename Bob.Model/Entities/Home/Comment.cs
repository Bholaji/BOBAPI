using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bob.Model.Entities.Home
{
	public class Comment: BaseEntity
	{
		[MaxLength(500)]
		public string CommentBody { get;set; }
		public Guid UserId { get; set; }
		[ForeignKey(nameof(UserId))]
		[ValidateNever]
		public User user { get; set; }
		public Guid PostId { get; set; }
		[ForeignKey(nameof(PostId))]
		[ValidateNever]
		public Post post { get;}
	}
}
