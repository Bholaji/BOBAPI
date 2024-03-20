using Bob.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Model.Entities
{
	public class LeaveDaysAccural : BaseLeaveEntity
	{
		public Guid UserId { get; set; }
		public DateTime AccuralDate { get; set; }
		public string AccuralPeriod { get; set; }
		public double Amount { get; set; }
		public string Note { get; set; }
		public LeavePolicy ActivityType { get; set; }
	}
}
