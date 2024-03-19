using Bob.Model.Enums;

namespace Bob.Model.Entities
{
	public class LeaveBalanceActivity
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public LeavePolicy ActivityType { get; set; }
	}
}
