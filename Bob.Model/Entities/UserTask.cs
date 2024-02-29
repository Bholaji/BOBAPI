using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskStatus = Bob.Model.Enums.TaskStatus;

namespace Bob.Model.Entities
{
	public class UserTask : BaseEntity
	{
        public Guid TaskJobId { get; set; }
		[ValidateNever]
		[ForeignKey("TaskJobId")]
		public TaskJob TaskJobs { get; set; }
		[Required]
        public Guid RequestedForId { get; set; }
		[Required]
		public Guid RequestedById { get; set; }
		public TaskStatus TaskStatus { get; set; }
	}
}
