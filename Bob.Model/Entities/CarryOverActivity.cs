using Bob.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Model.Entities
{
	public class CarryOverActivity : BaseLeaveEntity
	{
		public DateTime EffectiveDate { get; set; }
		public double Amount { get; set; }
		[MaxLength(500)]
		public string? Description { get; set; }
		public DateTime UpdatedOn { get; set; }
		public Guid UserId { get; set; }
		public LeavePolicy ActivityType { get; set; }
	}
}
