using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Model.Entities.Home
{
	public class Post : BaseEntity
	{
		[MaxLength(50)]
		public string Title { get; set; }
		[MaxLength(500)]
		public string Content { get; set; }
		public string? ImageUrl { get; set; }
		public Guid UserId { get; set; }
		[ForeignKey(nameof(UserId))]
		[ValidateNever]
		public User user { get; set; }
	}
}
