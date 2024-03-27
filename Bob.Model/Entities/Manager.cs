using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bob.Model.Entities
{
	public class Manager: BaseEntity
	{
		public Guid UserId { get; set; }

		[ValidateNever]
		[ForeignKey("UserId")]
		public User User { get; set; }
	}
}
