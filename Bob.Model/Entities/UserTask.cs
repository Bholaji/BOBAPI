using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskStatus = Bob.Model.Enums.TaskStatus;

namespace Bob.Model.Entities
{
	public class UserTask : BaseEntity
	{
		[MaxLength(50)]
        public string TaskName { get; set; }
		[Required]
        public Guid RequestedForId { get; set; }
		[Required]
		public Guid RequestedById { get; set; }
		[MaxLength(200)]
		public string TaskDescription { get; set; }
		[MaxLength(50)]
		public string TaskList { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }
		public TaskStatus TaskStatus { get; set; }
	}
}
