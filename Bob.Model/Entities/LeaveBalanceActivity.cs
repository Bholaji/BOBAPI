using Bob.Model.Enums;

namespace Bob.Model.Entities
{
	public class LeaveBalanceActivity : BaseLeaveEntity
	{
		public Guid UserId { get; set; }
		public LeavePolicy ActivityType { get; set; }
	}
}
