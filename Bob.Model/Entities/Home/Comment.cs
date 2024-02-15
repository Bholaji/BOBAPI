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
	public class Comment: BaseEntity
	{
		[MaxLength(500)]
		public string CommentBody { get;set; }
		public Guid UserId { get; set; }
		[ForeignKey(nameof(UserId))]
		[ValidateNever]
		public User user { get; set; }
	}
}
