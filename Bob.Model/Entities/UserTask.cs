using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskStatus = Bob.Model.Enums.TaskStatus;

namespace Bob.Model.Entities
{
	public class UserTask
	{
		[Key]
        public Guid TaskId { get; set; }
        public string TaskName { get; set; }
        public string RequestedFor { get; set; }
        public string TaskDescription { get; set; }
        public string TaskList { get; set; }
        public DateOnly DueDate { get; set; }
        public DateOnly StartDate { get; set; }
		public TaskStatus TaskStatus { get; set; }
		public Guid? UserId { get; set; }
		[ForeignKey("UserId")]
		[ValidateNever]
		public User User { get; set; }
	}
}
